using BBG.Dueling.Actions;
using BBG.ValueGetters;
using System;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class DestroySubstitute : CardActionModifier
    {
        public IntGetter maxPerTurn;
        public Reason[] reasons;
        public Effect[] insteadEffects;

        private int resolvedThisTurn;

        protected override EventName Event => EventName.DestroyAction;

        protected override bool IsValid(DuelAction action)
        {
            var destroy = action as DestroyAction;
            if (reasons.Length > 0 && !reasons.Contains(destroy.reason))
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            if (action.beforePerformDelegates.ContainsKey(GetHashCode()))
                return;

            void beforePerform()
            {
                if (maxPerTurn != null && resolvedThisTurn >= maxPerTurn.GetValue(this))
                    return;
                if (insteadEffects != null && insteadEffects.Any(e => !e.CanResolve()))
                    return;

                new ShowResolveAction(card).Perform(Priority.AboveDefault);
                action.Cancel();
                insteadEffects.Foreach(e => e.Resolve());
                resolvedThisTurn++;
            }
            action.beforePerformDelegates[GetHashCode()] = beforePerform;
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            insteadEffects = insteadEffects.Clone(card);
            Events.Observe(EventName.ChangeTurnAction, OnEndTurn);
        }

        protected override void OnRevert()
        {
            base.OnRevert();
            insteadEffects.Foreach(e => e.Revert());
            Events.RemoveObserve(EventName.ChangeTurnAction, OnEndTurn);
        }

        private void OnEndTurn(DuelAction action)
        {
            resolvedThisTurn = 0;
        }
    }
}