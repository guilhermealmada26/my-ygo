using BBG.Dueling.Actions;
using BBG.Dueling.Selection;

namespace BBG.Dueling.Effects
{
    public class DirectAttack : CardActionModifier
    {
        public bool makeSelection;
        const string message = "Do you want to attack directly?";

        protected override EventName Event => EventName.AttackAction;

        protected override bool IsValid(DuelAction action)
        {
            var attack = action as AttackAction;
            if (attack.IsDirect)
                return false;
            if (attack.beforePerformDelegates.ContainsKey(GetHashCode()))
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var attack = action as AttackAction;
            if (makeSelection)
            {
                var selection = new YesNoSelection(attack.Attacker.Controller, message);
                attack.beforePerformDelegates[GetHashCode()] = selection.Perform;
                selection.AfterSelection += (s) =>
                {
                    attack.IsDirect = selection.choice.Value;
                };
            }
            else
            {
                attack.IsDirect = true;
            }
        }
    }
}