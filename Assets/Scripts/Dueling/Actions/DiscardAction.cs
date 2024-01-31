namespace BBG.Dueling.Actions
{
    public class DiscardAction : MoveCardAction
    {
        public DiscardAction(Card card, Card caller, Reason reason) : base(card, Location.Graveyard)
        {
            moveOnlyIfValid = true;
            Caller = caller;
            this.reason = reason;
        }

        protected override bool ValidPerform()
        {
            if (card.Location is Location.Graveyard or Location.Banished)
                return false;
            return base.ValidPerform();
        }
    }
}