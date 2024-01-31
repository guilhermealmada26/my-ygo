using BBG.Conditions;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/ConditionalCardsGetter")]
    public class ConditionalCardsGetter : CardsGetter
    {
        [System.Serializable]
        public class Pair
        {
            public Condition condition;
            public CardsGetter cardGetter;
        }
        public Pair[] alternatives;
        public CardsGetter defaultGetter;

        public override List<Card> GetCards(Effect caller)
        {
            return defaultGetter.GetCards(caller);
        }

        public override List<Card> GetCards(Effect caller, Card toBeChecked)
        {
            return GetCardsGetter(caller, toBeChecked).GetCards(caller);
        }

        private CardsGetter GetCardsGetter(Effect caller, Card toBeChecked)
        {
            var args = new FilterArgs(toBeChecked, caller);
            foreach (var pair in alternatives)
            {
                if (pair.condition.IsValid(args))
                    return pair.cardGetter;
            }
            return defaultGetter;
        }
    }
}