using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    public class SameBattleDamage : ActionModifier
    {
        protected override EventName Event => EventName.BattleAction;

        protected override bool IsValid(DuelAction action)
        {
            var battle = action as BattleAction;
            if (battle.attacked != card && battle.attacker != card)
                return false;
            if (battle.DamageValue == 0)
                return false;
            if (battle.DamagedPlayer != player)
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var battle = action as BattleAction;
            var opp = player.Opponent;
            var damage = new ChangeLPAction(opp, Mathf.Abs(battle.DamageValue), Reason.Effect, card);
            battle.damages.Insert(0, damage);
        }
    }
}