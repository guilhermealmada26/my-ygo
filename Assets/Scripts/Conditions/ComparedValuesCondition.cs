using BBG.Dueling;
using BBG.Dueling.Effects;
using BBG.Numbers;
using BBG.ValueGetters;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/ComparisonCondition")]
    public class ComparedValuesCondition : CardCondition
    {
        public Comparison comparison;
        public IntGetter value1, value2;

        protected override bool IsValid(Card card, Player player, Effect effect)
        {
            var v1 = value1.GetValue(card);
            var v2 = value2.GetValue(card);
            return comparison.IsValid(v1, v2);
        }
    }
}