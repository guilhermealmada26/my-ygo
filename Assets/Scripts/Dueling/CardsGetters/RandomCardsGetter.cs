using BBG.Conditions;
using BBG.Dueling.Effects;
using BBG.ValueGetters;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/RandomCardsGetter")]
    public class RandomCardsGetter : CardsGetter
    {
        public CardsGetter defaultGetter;
        public Condition[] filter;
        public IntGetter intGetter;

        public override List<Card> GetCards(Effect caller)
        {
            var amount = intGetter.GetValue(caller);
            var cards = defaultGetter.GetCards(caller);
            cards.Filter(caller, filter);
            if (cards.Count == 0 || cards.Count < amount)
                return new List<Card>(0);
            if (cards.Count == amount)
                return cards;

            var selected = new List<Card>(amount);
            while (selected.Count < amount)
            {
                var card = cards.GetRandom(Duel.Current.Random);
                selected.Add(card);
                cards.Remove(card);
            }
            return selected;
        }
    }
}