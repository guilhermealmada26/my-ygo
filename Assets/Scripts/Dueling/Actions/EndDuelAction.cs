namespace BBG.Dueling.Actions
{
    public class EndDuelAction : DuelAction
    {
        public enum Reason
        {
            LifeEnded, CardsEnded, CardEffect, Disconnection, Surrender, ReplayEnded
        }
        public Reason winReason;
        public Player winner;
        public Card card;

        public EndDuelAction(Reason winReason, Player winner = null, Card card = null)
        {
            this.winReason = winReason;
            this.winner = winner;
            this.card = card;
            priority = Priority.AboveAll;
        }

        protected override void OnPerform()
        {
            duel.IsPlaying = false;
        }

        public bool DuelWasComplete => winReason is not Reason.Disconnection or Reason.ReplayEnded;
    }
}