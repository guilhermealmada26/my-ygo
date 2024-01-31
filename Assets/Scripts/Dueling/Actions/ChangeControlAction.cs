namespace BBG.Dueling.Actions
{
    public class ChangeControlAction : MoveCardAction, IRevertable
    {
        public Player newController;

        public ChangeControlAction(Card card, Location destination, Card caller)
            : base(card, destination)
        {
            newController = caller.Controller;
            if (newController == card.Controller)
                newController = newController.Opponent;
            Caller = caller;
            reason = Reason.Effect;
        }

        protected override bool ValidPerform()
        {
            if (destination.IsField() && newController[destination].Count >= newController.FieldMaxCards)
                return false;
            return true;
        }

        protected override void OnPerform()
        {
            card.SetLocation(destination, newController);
        }

        public void Revert()
        {
            if (card.Location != destination)
                return;
            newController = newController.Opponent;
            if (destination.IsField() && newController[destination].Count >= newController.FieldMaxCards)
                destination = Location.Graveyard;
            ProcessPerform();
        }
    }
}