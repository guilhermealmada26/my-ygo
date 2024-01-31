using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/MonsterType")]
    public class MonsterTypeCondition : CardDataCondition
    {
        public MonsterType[] types;

        protected override bool IsValid(CardData data)
        {
            if (types.Length == 0)
                return true;
            if (data is not MonsterData monster)
                return false;
            return types.Contains(monster.monsterType);
        }
    }
}