using BBG.Conditions;
using BBG.Dueling.Actions;
using System.Linq;

namespace BBG.Dueling.Effects
{
    public class AlterDestinationEffect : PlayerActionModifier
    {
        public Location[] toCheck;
        public Location newLocation;
        public Condition[] filter;

        protected override EventName Event => EventName.CardMovedAction;

        protected override bool IsValid(DuelAction action)
        {
            var move = action as MoveCardAction;
            if (toCheck.Length > 0 && !toCheck.Contains(move.destination))
                return false;
            if (!move.card.IsValid(this, filter))
                return false;
            return base.IsValid(action);
        }

        protected override void OnValidateAction(DuelAction action)
        {
            var move = action as MoveCardAction;
            move.destination = newLocation;
        }
    }
}