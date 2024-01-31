using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/DiscardEffect")]
    public class DiscardEffect : CardsActionEffect
    {
        public Reason reason = Reason.Effect;

        protected override DuelAction GetAction(Card target)
        {
            return new DiscardAction(target, card, reason);
        }
    }
}