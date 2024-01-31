using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/Limitation")]
    public class CardLimitationCondition : CardDataCondition
    {
        public CardLimitation[] limitations;

        protected override bool IsValid(CardData data)
        {
            if (limitations.Length == 0)
                return true;
            return limitations.Contains(data.limitation);
        }
    }
}