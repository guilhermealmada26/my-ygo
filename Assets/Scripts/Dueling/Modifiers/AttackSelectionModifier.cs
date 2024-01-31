using BBG.Conditions;
using BBG.Dueling.Actions;
using BBG.Dueling.Selection;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/AttackSelectionModifier")]
    public class AttackSelectionModifier : PlayerActionModifier
    {
        public bool opponentSelects;
        public Condition[] notTargatable;

        protected override EventName Event => EventName.AttackSelection;

        protected override bool IsResolvable() => false;

        protected override void OnValidateAction(DuelAction action)
        {
            var selection = action as AttackSelection;
            if (!IsValid(selection.player))
                return;

            if (opponentSelects)
            {
                selection.player = selection.player.Opponent;
                selection.optional = false;
                new ShowResolveAction(card).Perform(Priority.AboveDefault);
            }
            if (notTargatable.Length > 0)
            {
                selection.AttackTargets.Filter(c => !c.IsValid(this, notTargatable));
            }
        }
    }
}