using BBG.Dueling.Actions;

namespace BBG.Dueling.Effects
{
    public class PiercingDamage : CardActionModifier
    {
        protected override EventName Event => EventName.BattleAction;

        protected override bool IsValid(DuelAction action)
        {
            var battle = action as BattleAction;
            if (battle.IsDirectAttack)
                return false;
            if (battle.attacked.InAtkPosition() || battle.DamageValue < 1)
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var battle = action as BattleAction;
            var opp = battle.attacked.Controller;
            battle.damages.Add(new ChangeLPAction(opp, battle.DamageValue, Reason.Battle, battle.attacker)
            {
                CallerObject = this,
            });
        }
    }
}