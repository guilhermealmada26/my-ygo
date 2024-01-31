namespace BBG.Dueling.Actions
{
    public class TributeAction : MoveCardAction
    {
        public Player player;

        public TributeAction(Card card, Reason reason, Card caller) : base(card, Location.Graveyard)
        {
            Caller = caller;
            this.reason = reason;
            player = caller.Controller;
        }

        public TributeAction(Card card, Reason reason, Player player) : base(card, Location.Graveyard)
        {
            Caller = null;
            this.reason = reason;
            this.player = player;
        }
    }
}