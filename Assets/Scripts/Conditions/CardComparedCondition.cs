using BBG.Dueling;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using System.Collections.Generic;

namespace BBG.Conditions
{
    public abstract class CardComparedCondition : Condition
    {
        public bool allowEmptyList;
        public CardsGetter cardsGetter;
        public Condition[] cardsFilter;

        public override bool IsValid(object args)
        {
            if (args is FilterArgs filter)
            {
                var cards = cardsGetter.GetCards(filter.caller);
                cards.Filter(filter.caller, cardsFilter);
                if(cards.Count == 0)
                    return allowEmptyList;
                return IsValid(filter.target, cards);
            }

            return false;
        }

        protected abstract bool IsValid(Card other, List<Card> cards);
    }
}