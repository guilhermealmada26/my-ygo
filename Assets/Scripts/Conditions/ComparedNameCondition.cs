using BBG.Dueling;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/ComparedNameCondition")]
    public class ComparedNameCondition : CardComparedCondition
    {
        protected override bool IsValid(Card other, List<Card> cards)
        {
            if (cards == null || cards.Count == 0)
                return true;
            var card = cards[0];
            return card.data.cardName == other.data.cardName;
        }
    }
}