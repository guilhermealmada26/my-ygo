using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/OnEventsReapply")]
    public class OnEventsReapply : OnEventsPerformed
    {
        protected override void ActionPerformed(DuelAction action)
        {
            if (effect is IReappliable reappliable)
                reappliable.Reapply();
        }
    }
}