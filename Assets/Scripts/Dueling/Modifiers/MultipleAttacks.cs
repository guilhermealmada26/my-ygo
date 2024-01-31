using BBG.Dueling.Actions;
using BBG.ValueGetters;

namespace BBG.Dueling.Effects
{
    public class MultipleAttacks : CardActionModifier
    {
        public IntGetter valueGetter;
        public bool onceIfDirect;

        protected override EventName Event => EventName.AttackAction;

        protected override void OnValidateAction(DuelAction action)
        {
            var attack = action as AttackAction;
            if (attack.IsDirect && onceIfDirect)
                attack.MaxAttacks = 1;
            else
                attack.MaxAttacks = valueGetter.GetValue(this);
        }
    }
}