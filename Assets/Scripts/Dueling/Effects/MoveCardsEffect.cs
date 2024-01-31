using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/MoveCards")]
    public class MoveCardsEffect : CardsActionEffect
    {
        public Location destination;
        public Reason reason = Reason.Effect;

        protected override DuelAction GetAction(Card target)
        {
            return new MoveCardAction(target, destination, card)
            {
                reason = reason
            };
        }
    }
}