using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/ReverseCondition")]
    public class ReverseCondition : Condition
    {
        public Condition condition;

        public override bool IsValid(object args)
        {
            return !condition.IsValid(args);
        }
    }
}