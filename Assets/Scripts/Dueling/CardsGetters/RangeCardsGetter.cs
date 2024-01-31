using BBG.Dueling.Effects;
using BBG.ValueGetters;
using System.Collections.Generic;

namespace BBG.Dueling.CardsGetters
{
    public class RangeCardsGetter : CardsGetter
    {
        public CardsGetter defaultGetter;
        public IntGetter amount;

        public override List<Card> GetCards(Effect caller)
        {
            var value = amount.GetValue(caller);
            var cards = defaultGetter.GetCards(caller);
            if (value >= cards.Count)
                return cards;
            return cards.GetRange(0, value);
        }
    }
}