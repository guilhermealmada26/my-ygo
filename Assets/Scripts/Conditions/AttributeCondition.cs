using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/MonsterAttribute")]
    public class AttributeCondition : CardDataCondition
    {
        public MonsterAttribute[] attributes;

        protected override bool IsValid(CardData data)
        {
            if (attributes.Length == 0)
                return true;
            if (data is not MonsterData monster)
                return false;
            return attributes.Contains(monster.attribute);
        }
    }
}