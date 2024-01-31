namespace BBG.Dueling.Actions
{
    public class FlipPositionAction : CardAction
    {
        public FlipPositionAction(Monster card) : base(card) { }

        protected override bool ValidPerform()
        {
            if (duel.Phase != DuelPhase.Main)
                return false;
            if (card.Location != Location.MonsterZone)
                return false;
            if (!card.IsFaceUp())
                return false;
            //if summoned this turn, cannot perform
            var summonThisTurn = TurnActionsRegistry.Instance.PerformedThisTurn<SummonAction>(card);
            if (summonThisTurn.Count > 0)
                return false;
            //if flipped this turn, cannot perform
            var flippedThisTurn = TurnActionsRegistry.Instance.PerformedThisTurn<FlipPositionAction>(card);
            if (flippedThisTurn.Count > 0)
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            card.position.Current = card.Position == CardPosition.AtkFaceUp ?
                CardPosition.DefFaceUp : CardPosition.AtkFaceUp;
        }
    }
}