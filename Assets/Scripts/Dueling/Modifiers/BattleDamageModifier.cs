using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.PlayersGetters;
using BBG.ValueGetters;

namespace BBG.Dueling.Effects
{
    public class BattleDamageModifier : ActionModifier
    {
        public PlayersGetter target;
        public Condition[] attackerFilter;
        public Condition[] attackedFilter;
        public IntGetter multiplier;

        protected override EventName Event => EventName.BattleAction;

        protected override bool IsValid(DuelAction action)
        {
            var battle = action as BattleAction;
            if (!battle.attacker.IsValid(this, attackerFilter))
                return false;
            if (!battle.attacked.IsValid(this, attackedFilter))
                return false;
            if (battle.DamagedPlayer == null)
                return false;
            if (!target.IsValid(this, battle.DamagedPlayer))
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var battle = action as BattleAction;
            battle.DamageValue *= multiplier.GetValue(this);
        }
    }
}