using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.AI
{
    [CreateAssetMenu(menuName = "Duel/AI/AttackWithAvailable")]
    public class AttackWithAvailable : AIDeclareAspect
    {
        public override DuelAction GetAction(Player player)
        {
            if (duel.TurnCount > 1 && duel.Phase != DuelPhase.Battle)
                return new ChangePhaseAction(DuelPhase.Battle);

            var monsters = player.GetMonsters(Location.MonsterZone);
            monsters.Filter(m => m.InAtkPosition() && new AttackAction(m).CanPerform());
            if (monsters.Count == 0)
                return null;

            var attack = new AttackAction(monsters.GetRandom());
            return attack;
        }
    }
}