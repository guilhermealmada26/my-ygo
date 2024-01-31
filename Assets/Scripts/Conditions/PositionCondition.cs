using BBG.Dueling;
using BBG.Dueling.Effects;
using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/PositionCondition")]
    public class PositionCondition : CardCondition
    {
        public CardPosition[] positions;
        public bool previousPosition;

        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            var position = previousPosition ? card.position.Previous : card.position.Current;
            return positions.Contains(position);
        }
    }
}