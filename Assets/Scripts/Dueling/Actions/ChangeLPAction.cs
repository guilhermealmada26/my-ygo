namespace BBG.Dueling.Actions
{
    public class ChangeLPAction : PlayerTargetAction
    {
        public int amount;

        public Reason Reason { internal set; get; }
        public int PreviousLP { private set; get; }

        public ChangeLPAction(Player player, int amount, Reason reason, Card caller) : base(player)
        {
            this.amount = amount;
            Reason = reason;
            Caller = caller;
        }

        protected override bool ValidPerform()
        {
            if (amount == 0)
                return false;
            if (Reason == Reason.Cost && player.LP - amount <= 0)
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            PreviousLP = player.LP;
            player.LP -= amount;

            if (player.LP <= 0)
            {
                var endDuel = new EndDuelAction(EndDuelAction.Reason.LifeEnded, player.Opponent);
                endDuel.Perform(Priority.AboveAll);
            }
        }
    }
}