using BBG.Dueling.Actions;
using BBG.ValueGetters;
using System.Linq;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Modifiers/ChangeLP")]
    public class ChangeLPModifier : PlayerActionModifier
    {
        public IntGetter newValue;
        public bool heal;
        public Reason[] reasons;

        protected override EventName Event => EventName.ChangeLPAction;

        protected override void OnValidateAction(DuelAction action)
        {
            var change = action as ChangeLPAction;
            if (!reasons.Contains(change.Reason))
                return;
            if (!heal && change.amount < 0)
                return;
            if (heal && change.amount > 0)
                return;
            if (!IsValid(change.player))
                return;

            change.amount = newValue.GetValue(this);
        }
    }
}