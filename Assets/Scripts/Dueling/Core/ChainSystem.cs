using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling
{
    public class ChainSystem : DuelComponent
    {
        readonly HashSet<Effect> respondables = new();
        readonly Stack<ResolveAction> chain = new();

        public ResolveAction Last => chain.Count == 0 ? null : chain.Peek();
        public bool IsResolving { get; private set; }

        public bool CanAddLink(Effect effect)
        {
            if (IsResolving)
                return false;
            if (chain.Count == 0)
                return true;
            if (IsChained(effect))
                return false;
            return Last.effect.ValidRespondable(effect);
        }

        internal int AddLink(Effect effect)
        {
            chain.Push(new ResolveAction(effect));
            new CheckResponds(respondables, effect.player.Opponent).Perform(Priority.ChainLink);
            if (chain.Count == 1)
            {
                //these are processed only once when the first chain link is added, resolved after all activations and selections
                new DelegateAction(ResolveChain).Perform(Priority.ResolveChain);
            }
            return chain.Count;
        }

        private void ResolveChain()
        {
            foreach (var link in chain)
                link.Perform(Priority.ChainLink);
            chain.Clear();
            IsResolving = true;
            new DelegateAction(OnChainEnd).Perform(Priority.ChainResolved);
        }

        private void OnChainEnd()
        {
            IsResolving = false;
        }

        internal bool IsChained(Effect effect) => chain.Any(l => effect == l.effect);

        public void AddRespondable(Effect effect)
        {
            respondables.Add(effect);
        }

        public void RemoveRespondable(Effect effect)
        {
            respondables.Remove(effect);
        }
    }
}