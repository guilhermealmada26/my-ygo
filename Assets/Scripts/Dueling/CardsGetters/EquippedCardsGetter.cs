using BBG.Conditions;
using BBG.Dueling.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling.CardsGetters
{
    [CreateAssetMenu(menuName = "CardsGetters/EquippedCardsGetter")]
    public class EquippedCardsGetter : CardsGetter
    {
        public bool getEquipTarget;
        public Condition[] callerFilter;

        public override List<Card> GetCards(Effect effect)
        {
            return GetCards(effect, effect.card);
        }

        public override List<Card> GetCards(Effect effect, Card toBeChecked)
        {
            if (getEquipTarget && effect.card.equippedTo != null)
                return new List<Card>(1) { effect.card.equippedTo.card };
            var cards = new List<Card>(4);
            foreach (var action in toBeChecked.equips)
            {
                if (callerFilter != null && !action.Caller.IsValid(effect, callerFilter))
                    continue;
                cards.Add(action.equip);
            }
            return cards;
        }
    }
}