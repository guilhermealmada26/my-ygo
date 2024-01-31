using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/ChangePosition")]
    public class ChangePositionEffect : CardsActionEffect
    {
        public CardPosition position;

        protected override DuelAction GetAction(Card target)
        {
            return new ChangePositionAction(target, position, card);
        }
    }
}