namespace BBG.Dueling.Actions
{
    public class ChangePhaseAction : DuelAction
    {
        public DuelPhase nextPhase;

        public ChangePhaseAction(DuelPhase nextPhase)
        {
            this.nextPhase = nextPhase;
        }

        protected override bool ValidPerform()
        {
            if (nextPhase == DuelPhase.Battle && duel.TurnCount == 1)
                return false;
            if (nextPhase == duel.Phase)
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            duel.Phase = nextPhase;

            if (nextPhase == DuelPhase.End)
            {
                new ChangeTurnAction().Perform(Priority.AfterTriggers);
                new ChangePhaseAction(DuelPhase.Draw).Perform(Priority.AfterTriggers);
            }
            else if (nextPhase == DuelPhase.Draw)
            {
                new DrawCardsAction(duel.CurrentPlayer, 1, Reason.DuelPhase).Perform(Priority.AfterTriggers);
                new ChangePhaseAction(DuelPhase.Main).Perform(Priority.AfterTriggers);
            }
        }
    }
}