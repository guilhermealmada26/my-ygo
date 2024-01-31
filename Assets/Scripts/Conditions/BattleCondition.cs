using BBG.Dueling;
using BBG.Dueling.Actions;
using BBG.Dueling.Effects;
using BBG.Numbers;
using BBG.ValueGetters;

namespace BBG.Conditions
{
    public class BattleCondition : Condition
    {
        public bool directAttackOnly;
        public bool selfBattled;
        public Comparison damageComparison;
        public IntGetter toCompare;

        public override bool IsValid(object args)
        {
            if (args is not Effect effect)
                return false;
            var action = effect.TriggerAction;
            if (action == null || action is not BattleAction battle)
                return false;
            if (directAttackOnly && !battle.IsDirectAttack)
                return false;
            if (selfBattled && !CardBattled(effect.card, battle))
                return false;
            if (toCompare != null && !damageComparison.IsValid(battle.DamageValue, toCompare.GetValue(args)))
                return false;
            return true;
        }

        private bool CardBattled(Card card, BattleAction battle)
        {
            return card == battle.attacker || card == battle.attacked;
        }
    }
}