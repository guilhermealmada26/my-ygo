namespace BBG.Dueling.Actions
{
    public class ChangePositionAction : CardAction
    {
        public CardPosition position;

        public ChangePositionAction(Card card, CardPosition position, Card caller = null) : base(card)
        {
            this.position = position;
            Caller = caller;
            reason = Reason.Effect;
        }

        /// <summary>
        /// Position is flipped
        /// </summary>
        public ChangePositionAction(Card card) : base(card)
        {
            switch (card.Position)
            {
                case CardPosition.AtkFaceUp:
                    position = CardPosition.AtkFaceDown; break;
                case CardPosition.AtkFaceDown:
                    position = CardPosition.AtkFaceUp; break;
                case CardPosition.DefFaceUp:
                    position = CardPosition.DefFaceDown; break;
                case CardPosition.DefFaceDown:
                    position = CardPosition.DefFaceUp; break;
            }
        }

        protected override void OnPerform()
        {
            card.position.Current = position;
        }
    }
}