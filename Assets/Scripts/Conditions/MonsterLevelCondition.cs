using BBG.Ygo;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/CardData/MonsterLevel")]
    public class MonsterLevelCondition : CardDataCondition
    {
        public int[] levels;

        protected override bool IsValid(CardData data)
        {
            if (levels.Length == 0)
                return true;
            if (data is not MonsterData monster)
                return false;
            return levels.Contains(monster.lvl);
        }
    }
}