using BBG.Dueling;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/CardsIntGetterSum")]
    public class CardsIntGetterSum : DuelIntGetter
    {
        public DuelIntGetter intGetter;
        public CardsGetter cardsGetter;

        protected override int GetValue(Card card, Effect caller)
        {
            var cards = cardsGetter.GetCards(caller);
            int sum = 0;
            foreach (var c in cards)
                sum += intGetter.GetValue(new FilterArgs(c, caller));
            return sum;
        }
    }
}