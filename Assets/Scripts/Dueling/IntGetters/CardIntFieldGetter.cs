using BBG.Dueling;
using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/CardIntFieldGetter")]
    public class CardIntFieldGetter : DuelIntGetter
    {
        public CardIntField field;
        public bool originalValue;
        public float multiplier = 1f;
        public CardsGetter overrideGetter;

        protected override int GetValue(Card card, Effect caller)
        {
            if (overrideGetter != null)
            {
                var cards = overrideGetter.GetCards(caller);
                if(cards.Count == 0)
                    return 0;
                card = cards[0];
            }
            var value = originalValue ? field.GetOriginalValue(card) : field.GetValue(card);
            return (int)(value * multiplier);
        }
    }
}