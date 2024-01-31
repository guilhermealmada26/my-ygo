using BBG.Numbers;
using BBG.ValueGetters;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/NumberCondition")]
    public class NumberCondition : Condition
    {
        public IntGetter valueGetter, toCompareGetter;
        public Comparison comparison;

        public override bool IsValid(object args)
        {
            var value = valueGetter.GetValue(args);
            var toCompare = toCompareGetter.GetValue(args);
            return comparison.IsValid(value, toCompare);
        }
    }
}