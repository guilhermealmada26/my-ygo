namespace BBG.Dueling
{
    public enum CardPosition
    {
        AtkFaceDown, AtkFaceUp, DefFaceDown, DefFaceUp
    }

    static class CardPositionExtensions
    {
        public static bool IsFaceUp(this CardPosition position)
        {
            return position is CardPosition.AtkFaceUp or CardPosition.DefFaceUp;
        }

        public static bool InAtkPosition(this CardPosition position)
        {
            return position is CardPosition.AtkFaceDown or CardPosition.AtkFaceUp;
        }

        public static bool IsFaceUp(this Card card)
        {
            return card.Position.IsFaceUp();
        }

        public static bool InAtkPosition(this Card card)
        {
            return card.Position.InAtkPosition();
        }

        public static CardPosition GetFlippedPosition(this Card card)
        {
            return card.Position switch
            {
                CardPosition.AtkFaceDown => CardPosition.AtkFaceUp,
                CardPosition.AtkFaceUp => CardPosition.AtkFaceDown,
                CardPosition.DefFaceDown => CardPosition.DefFaceUp,
                _ => CardPosition.DefFaceDown,
            };
        }
    }
}