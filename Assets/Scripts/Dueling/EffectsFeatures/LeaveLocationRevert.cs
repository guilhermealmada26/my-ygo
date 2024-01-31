using UnityEngine;

namespace BBG.Dueling.Effects
{
    [CreateAssetMenu(menuName = "Effects/Features/LeaveLocationRevert")]
    public class LeaveLocationRevert : OnLocationFeature
    {
        protected override void OnApply()
        {
        }

        protected override void OnUnApply()
        {
            effect.Revert();
        }
    }
}