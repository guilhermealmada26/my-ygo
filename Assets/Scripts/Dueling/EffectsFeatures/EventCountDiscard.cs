using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/EventCountDiscard")]
    public class EventCountDiscard : EventCountAction
    {
        protected override void OnCountReached()
        {
            if (effect.card is Monster)
                new DiscardAction(effect.card, effect.card, Reason.Other).Perform();
            else
                new DiscardResolved(effect.card).Perform();
        }
    }
}