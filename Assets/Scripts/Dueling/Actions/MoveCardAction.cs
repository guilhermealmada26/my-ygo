namespace BBG.Dueling.Actions
{
    public class MoveCardAction : CardAction
    {
        public Location destination;
        protected bool moveOnlyIfValid;
        public Player playerTarget;

        public MoveCardAction(Card card, Location location) : base(card)
        {
            destination = location;
        }

        public MoveCardAction(Card card, Location location, Card caller) : base(card)
        {
            destination = location;
            Caller = caller;
            reason = Reason.Effect;
        }

        protected override bool ValidPerform()
        {
            Events.TriggerValidate(this, EventName.CardMovedAction);
            if (card.Location == destination)
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            if (moveOnlyIfValid && !CanPerform())
                return;
            card.SetLocation(destination, playerTarget);
            CheckFieldSpell();
            card.LastMove = this;
            Events.TriggerAction(this, EventName.CardMovedAction.ToString());
        }

        private void CheckFieldSpell()
        {
            var field = card.Controller[Location.FieldZone];
            if (card.Location != Location.FieldZone || field.Count < 2)
                return;

            new MoveCardAction(field[0], Location.Graveyard).Perform();
        }
    }
}