using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/EventCountRevert")]
    public class EventCountRevert : EventCountAction
    {
        public bool delay;

        protected override void OnCountReached()
        {
            if (delay)
                new DelegateAction(effect.Revert).Perform();
            else
                effect.Revert();
        }
    }
}