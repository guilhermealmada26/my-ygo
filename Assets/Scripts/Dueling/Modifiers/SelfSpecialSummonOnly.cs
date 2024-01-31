using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class SelfSpecialSummonOnly : ActionModifier
    {
        protected override EventName Event => EventName.SpecialSummon;

        protected override bool IsValid(DuelAction action)
        {
            var summon = action as SummonAction;
            if (summon.card != card)
                return false;
            if (summon.Caller == card)
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            action.Cancel();
        }
    }
}