using BBG.Dueling.Actions;
using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/OnMovedActivate")]
    public class OnMovedActivate : OnLocationFeature
    {
        protected override void OnApply()
        {
            void action() => new ActivationAction(effect).Perform();
            new DelegateAction(action).Perform(Priority.AfterChain);
        }

        protected override void OnUnApply()
        {
        }
    }
}