using BBG.Dueling.CardsGetters;
using BBG.Dueling.Effects;
using BBG.ValueGetters;
using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/HasCardsCondition")]
    public class HasCardsCondition : Condition
    {
        public CardsGetter cardsGetter;
        public CardData[] validNames;
        public Condition[] conditions;
        public IntGetter required;
        public bool hasLessThan;

        public override bool IsValid(object args)
        {
            if (args is not Effect && args is not FilterArgs)
                return false;
            var effect = (args is Effect) ? args as Effect : (args as FilterArgs).caller;
            var cards = cardsGetter.GetCards(effect);
            if (cards.Count > 0)
                if (validNames.Length > 0)
                    cards.Filter(c => validNames.Any(n => n.cardName == c.data.cardName));
            cards.Filter(c => conditions.All(f => f.IsValid(new FilterArgs(c, effect))));
            if (hasLessThan)
                return cards.Count < required.GetValue(effect);
            return cards.Count >= required.GetValue(effect);
        }
    }
}