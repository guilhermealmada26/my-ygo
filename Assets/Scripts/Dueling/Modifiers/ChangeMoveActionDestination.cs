using BBG.Dueling.Actions;
using System;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class ChangeMoveActionDestination : CardActionModifier
    {
        public Reason[] reasons;
        public Location location;
        public EventName eventName;

        protected override EventName Event => eventName;

        protected override bool IsValid(DuelAction action)
        {
            var move = action as MoveCardAction;
            if (reasons.Length > 0 && !reasons.Contains(move.reason))
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var move = action as MoveCardAction;
            move.destination = location;
        }
    }
}