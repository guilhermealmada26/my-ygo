namespace BBG.Dueling.Actions
{
    public class FlipSummon : SummonAction
    {
        public FlipSummon(Monster monster) : base(monster.Controller, monster)
        {
            position = CardPosition.AtkFaceUp;
        }

        protected override bool ValidPerform()
        {
            if (Monster.Position != CardPosition.DefFaceDown)
                return false;
            if (Monster.Location != Location.MonsterZone)
                return false;
            if (duel.Phase != DuelPhase.Main)
                return false;
            //if summoned this turn, cannot perform
            var summonThisTurn = TurnActionsRegistry.Instance.PerformedThisTurn<SummonAction>(card);
            if (summonThisTurn.Count > 0)
                return false;
            return base.ValidPerform();
        }
    }
}