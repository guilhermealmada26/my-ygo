namespace BBG.Dueling.Selection
{
    public class PositionSelection : SelectionAction
    {
        public Card card;
        public CardPosition[] availablePositions;
        public CardPosition? position;

        public PositionSelection(Player player, Card card)
            : base(player)
        {
            message = "Select the monster's position";
            this.card = card;
            availablePositions = new CardPosition[] { CardPosition.AtkFaceUp, CardPosition.DefFaceUp };
            AfterSelection += (s) =>
            {
                card.position.Current = position.Value;
            };
        }

        public override bool RequirementsAreDone()
        {
            return position.HasValue;
        }

        public override string GetLoadableData()
        {
            return ((int)position.Value).ToString();
        }

        protected override void SetLoadableData(string data)
        {
            position = (CardPosition)int.Parse(data);
        }
    }
}