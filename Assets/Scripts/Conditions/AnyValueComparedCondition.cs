using BBG.Dueling;
using BBG.Numbers;
using BBG.ValueGetters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Compared/AnyValue")]
    public class AnyValueComparedCondition : CardComparedCondition
    {
        public IntGetter intGetter;
        public Comparison comparison;

        //card has equal value as any card in cards list
        //used by metamorphosis conditions
        protected override bool IsValid(Card other, List<Card> cards)
        {
            return cards.Any(c => IsValid(other, c));
        }

        private bool IsValid(Card other, Card card)
        {
            var cardValue = intGetter.GetValue(card);
            var otherValue = intGetter.GetValue(other);
            return comparison.IsValid(cardValue, otherValue);
        }
    }
}