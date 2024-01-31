using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/CardName")]
    public class CardNameCondition : CardDataCondition
    {
        public string substring;
        public CardData[] cards;

        protected override bool IsValid(CardData data)
        {
            var name = data.cardName.ToLower();
            if (substring != string.Empty && !name.Contains(substring.ToLower()))
                return false;
            if(cards != null && cards.Length > 0 && !cards.Any(c =>  c.cardName.ToLower() == name))
                return false;
            return true;
        }
    }
}