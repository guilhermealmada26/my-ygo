namespace BBG.Dueling.Actions
{
    public class DestroyAction : MoveCardAction
    {
        public DestroyAction(Card card, Reason reason, Card caller) : base(card, Location.Graveyard)
        {
            moveOnlyIfValid = true;
            this.reason = reason;
            Caller = caller;
        }

        protected override bool ValidPerform()
        {
            if (card.Location is Location.Graveyard or Location.Banished) 
                return false;
            return base.ValidPerform();
        }
    }
}