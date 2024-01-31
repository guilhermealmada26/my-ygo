using BBG.Dueling;
using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/ControlCondition")]
    public class ControlCondition : CardCondition
    {
        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            return player == card.Controller;
        }
    }
}