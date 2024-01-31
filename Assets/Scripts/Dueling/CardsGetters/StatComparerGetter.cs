using BBG.Dueling.Effects;
using BBG.Numbers;
using BBG.ValueGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/StatComparer")]
    public class StatComparerGetter : CardsGetter
    {
        public CardsGetter cardsGetter;
        public IntGetter valueGetter;
        public Comparison comparison;

        public override List<Card> GetCards(Effect caller)
        {
            var cards = cardsGetter.GetCards(caller);
            if (caller is CardsEffect cardsEffect)
                cardsEffect.Filter(cards);

            if (cards.Count < 2)
                return cards;

            var choosen = cards[0];
            for (int i = 1; i < cards.Count; i++)
            {
                var card = cards[i];
                if (ValidComparison(choosen, card, caller, comparison))
                    choosen = card;
            }

            return CardsWithSameStat(choosen, cards, caller);
        }

        private bool ValidComparison(Card choosen, Card card, Effect caller, Comparison comparison)
        {
            var vChoosen = valueGetter.GetValue(new FilterArgs(choosen, caller));
            var vCard = valueGetter.GetValue(new FilterArgs(card, caller));
            return comparison.IsValid(vCard, vChoosen);
        }

        private List<Card> CardsWithSameStat(Card choosen, List<Card> cards, Effect caller)
        {
            var same = new List<Card>(2) { choosen };
            cards.Remove(choosen);
            foreach (var card in cards)
                if (ValidComparison(choosen, card, caller, Comparison.Equal))
                    same.Add(card);
            return same;
        }
    }
}