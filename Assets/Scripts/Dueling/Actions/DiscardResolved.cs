namespace BBG.Dueling.Actions
{
    public class DiscardResolved : MoveCardAction
    {
        public DiscardResolved(Card card) : base(card, Location.Graveyard)
        {
            moveOnlyIfValid = true;
        }

        protected override bool ValidPerform()
        {
            if (card.Location != Location.STZone)
                return false;
            return base.ValidPerform();
        }
    }
}