using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/CardsEffect/DestroyEffect")]
    public class DestroyEffect : CardsActionEffect
    {
        public Reason reason = Reason.Effect;

        protected override DuelAction GetAction(Card target)
        {
            return new DestroyAction(target, reason, card);
        }
    }
}