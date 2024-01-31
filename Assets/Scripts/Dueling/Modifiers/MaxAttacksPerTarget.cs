using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class MaxAttacksPerTarget : CardActionModifier
    {
        public int attacksPerTarget = 1;

        protected override EventName Event => EventName.AttackSelection;

        protected override bool IsValid(DuelAction action)
        {
            var selection = action as AttackSelection;
            if (selection.Attacker != card)
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var selection = action as AttackSelection;
            selection.AttackTargets.Filter(m => IsValid(card as Monster, m as Monster));
        }

        bool IsValid(Monster attacker, Monster target)
        {
            var attacksThisTurn = TurnActionsRegistry.Instance.PerformedThisTurn<BattleAction>();
            attacksThisTurn.Filter(a => a.attacker == attacker);
            var atks = attacksThisTurn.Where(a => a.attacked == target).ToArray();
            return atks.Length < attacksPerTarget;
        }
    }
}