using BBG.Ygo;
using System.Collections.Generic;

namespace BBG.Conditions
{
    public abstract class CardsFilter
    {
        public void Filter(List<CardData> cards)
        {
            cards.Filter(c => IsValid(c));
        }

        public abstract bool IsValid(CardData card);

        public List<CardData> GetCards(List<CardData> cards)
        {
            var _cards = new List<CardData>(cards);
            Filter(_cards);
            return _cards;
        }
    }
}