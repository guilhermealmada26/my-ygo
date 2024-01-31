using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Numbers;

namespace BBG.Dueling.Effects
{
    public abstract class CardActionModifier : PlayerActionModifier
    {
        public bool targetThisCard;
        public Condition[] targetFilters;
        public Condition[] callerFilters;

        protected override bool IsValid(DuelAction action)
        {
            if (action.Cancelled || action is not CardAction cardAction)
                return false;
            if (targetThisCard && cardAction.card != card)
                return false;
            if (!cardAction.card.IsValid(this, targetFilters))
                return false;
            var caller = action.Caller;
            if (callerFilters.Length > 0 && caller != null && !caller.IsValid(this, callerFilters))
                return false;
            if (CardIsUnnafected(cardAction.card))
                return false;
            return true;
        }

        protected override void OnValidateAction(DuelAction action)
        {
            action.Cancel();
        }
    }
}