using BBG.Conditions;
using BBG.Dueling;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/CardCountGetter")]
    public class CardCountGetter : DuelIntGetter
    {
        public CardsGetter cardsGetter;
        public Condition[] filters;
        public IntGetter multiplier;

        protected override int GetValue(Card card, Effect caller)
        {
            var cards = cardsGetter.GetCards(caller);
            cards.Filter(c => filters.All(f => f.IsValid(new FilterArgs(c, caller))));
            return cards.Count * multiplier.GetValue(caller);
        }
    }
}