using BBG.Dueling.Selection;

namespace BBG.Dueling.Effects
{
    public class ChangeAttackTargetEffect : SimpleEffect
    {
        private AttackSelection GetAction()
        {
            if(TriggerAction != null && TriggerAction is AttackSelection attackSelection)
                return attackSelection;
            return null;
        }

        protected override bool IsResolvable()
        {
            var action = GetAction();
            if(action == null || action.player == player || action.AttackTargets.Count < 2)
                return false;
            return base.IsResolvable();
        }

        protected override void OnResolve()
        {
            var action = GetAction();
            var newSelection = new AttackSelection(action.BattleAction)
            {
                player = player
            };
            newSelection.Perform();
        }
    }
}