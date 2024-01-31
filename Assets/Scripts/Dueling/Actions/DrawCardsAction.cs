using System.Collections.Generic;

namespace BBG.Dueling.Actions
{
    public class DrawCardsAction : PlayerTargetAction, ICardsHolder
    {
        public int amount;

        private List<Card> drawn;

        public DrawCardsAction(Player player, int amount, Reason reason) : base(player)
        {
            this.amount = amount;
            this.reason = reason;
        }

        public IEnumerable<Card> GetCards() => drawn;

        protected override bool ValidPerform()
        {
            var deck = player[Location.Deck];
            if (amount == 0 || deck.Count < amount)
                return false;
            return base.ValidPerform();
        }

        protected override void OnPerform()
        {
            drawn = new List<Card>(amount);
            var deck = player[Location.Deck];
            for (int i = 0; i < amount; i++)
            {
                var card = deck[deck.Count - 1];
                drawn.Add(card);
                card.SetLocation(Location.Hand);
            }
        }
    }
}