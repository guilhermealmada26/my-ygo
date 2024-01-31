using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/SpecialSummon")]
    public class SpecialSummonEffect : CardsActionEffect
    {
        public CardPosition[] position;
        public bool selectPosition;

        protected override DuelAction GetAction(Card target)
        {
            SpecialSummon action = new(player, target as Monster, card);
            if (position.Length > 0)
                action.position = position[0];
            else
                action.position = target.Position;
            action.selectPosition = selectPosition;
            return action;
        }

        protected override uint ClampMaxCards(uint max)
        {
            var emptySlots = player.FieldMaxCards - player[Location.MonsterZone].Count;
            if (emptySlots < max)
                return (uint)emptySlots;
            return max;
        }
    }
}