using System.Collections.Generic;

namespace BBG.Dueling.Actions
{
    public abstract class CardAction : DuelAction, ICardsHolder
    {
        public Card card { internal set; get; }

        protected CardAction(Card card)
        {
            this.card = card;
        }

        protected override bool ValidPerform()
        {
            return card != null && base.ValidPerform();
        }

        public virtual IEnumerable<Card> GetCards() => new Card[1] { card };
    }
}