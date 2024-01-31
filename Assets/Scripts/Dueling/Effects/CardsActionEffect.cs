using BBG.Dueling.Actions;
using System.Collections.Generic;

namespace BBG.Dueling.Effects
{
    public abstract class CardsActionEffect : CardsEffect
    {
        HashSet<IRevertable> revertables;

        protected override void OnSetup()
        {
            revertables = new HashSet<IRevertable>();
        }

        internal override void Resolve(List<Card> cards)
        {
            foreach (var target in cards)
            {
                var action = GetAction(target);
                action.CallerObject = this;
                action.Perform();
                if (action is IRevertable revertable)
                    revertables.Add(revertable);
            }
        }

        protected override void OnRevert()
        {
            foreach (var revertable in revertables)
                revertable.Revert();
            revertables.Clear();
        }

        internal override bool IsResolvable(Card target)
        {
            return GetAction(target).CanPerform();
        }

        protected abstract DuelAction GetAction(Card target);
    }
}