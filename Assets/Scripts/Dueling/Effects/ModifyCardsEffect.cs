using BBG.Dueling.Actions;
using System.Collections.Generic;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public abstract class ModifyCardsEffect : CardsEffect, IReappliable
    {
        public bool cumulativeResolve;

        readonly List<DuelAction> actions = new();

        internal override void Resolve(List<Card> cards)
        {
            if (!cumulativeResolve && actions.Count > 0)
                return;

            var newActions = new List<DuelAction>();
            cards.Foreach(c => AddActions(newActions, c));
            foreach (var action in newActions)
            {
                action.CallerObject = this;
                if (!action.CanPerform())
                    continue;
                action.ProcessPerform();
                actions.Add(action);
            }
        }

        protected abstract void AddActions(List<DuelAction> actions, Card target);

        protected override void OnRevert()
        {
            actions.Cast<IRevertable>().Foreach(action => action.Revert());
            actions.Clear();
        }

        public void Reapply()
        {
            OnRevert();
            OnResolve();
        }
    }
}