namespace BBG.Dueling.Actions
{
    public class SetCardAction : MoveCardAction
    {
        public Player player => card.Controller;

        public SetCardAction(Card card) : base(card, Location.STZone)
        {
        }

        protected override bool ValidPerform()
        {
            if (player[Location.STZone].Count >= player.FieldMaxCards)
                return false;
            if (priority == Priority.Manual)
            {
                if (duel.Phase != DuelPhase.Main)
                    return false;
                if (card.Location != Location.Hand)
                    return false;
            }
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            base.OnPerform();
            card.position.Current = CardPosition.AtkFaceDown;
        }
    }
}