using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/Triggable")]
    public class Triggable : OnLocationFeature
    {
        public EventName[] eventNames;

        protected override void OnApply()
        {
            var trigger = Duel.Current.GetComponent<TriggerEffectSystem>();
            eventNames.Foreach(name => trigger.Add(name, effect));
        }

        protected override void OnUnApply()
        {
            var trigger = Duel.Current.GetComponent<TriggerEffectSystem>();
            eventNames.Foreach(name => trigger.Remove(name, effect));
        }
    }
}