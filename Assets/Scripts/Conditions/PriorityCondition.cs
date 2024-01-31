using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/PriorityCondition")]
    public class PriorityCondition : Condition
    {
        public Condition defaultCondition, priorityCondition;
        [Header("If true, priority condition is used.")]
        public Condition comparer;

        public override bool IsValid(object args)
        {
            if (comparer.IsValid(args))
            {
                return priorityCondition.IsValid(args);
            }

            return defaultCondition.IsValid(args);
        }
    }
}