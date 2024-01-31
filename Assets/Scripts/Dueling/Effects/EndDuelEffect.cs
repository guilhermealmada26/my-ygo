using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/PlayerTarget/EndDuelEffect")]
    public class EndDuelEffect : PlayerActionEffect, IReappliable
    {
        public bool resolveImmediate;

        protected override DuelAction GetAction(Player target)
        {
            return new EndDuelAction(EndDuelAction.Reason.CardsEnded, target);
        }

        public void Reapply()
        {
            if (!CanResolve())
                return;

            new ActivationAction(this).Perform(resolveImmediate ? Priority.AboveDefault : Priority.Default);
        }
    }
}