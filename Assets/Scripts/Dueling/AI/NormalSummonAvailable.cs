using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/AI/NormalSummonAvailable")]
    public class NormalSummonAvailable : AIDeclareAspect
    {
        public int maxLevel;

        public override DuelAction GetAction(Player player)
        {
            var monsters = player.GetMonsters(Location.Hand);
            monsters.Filter(m => m.Lvl <= maxLevel && new NormalSummon(m).CanPerform());
            if (monsters.Count == 0)
                return null;
            return new NormalSummon(monsters.GetRandom());
        }
    }
}