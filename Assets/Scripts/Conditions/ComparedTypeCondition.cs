using BBG.Dueling;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Compared/CardType")]
    public class ComparedTypeCondition : CardComparedCondition
    {
        public bool mainType, subType;

        protected override bool IsValid(Card other, List<Card> cards)
        {
            var card = cards[0];
            if (mainType && other.Type.mainType != card.Type.mainType)
                return false;
            if(subType && other.Type.subtype != card.Type.subtype) 
                return false;
            return true;
        }
    }
}