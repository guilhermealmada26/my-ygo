using System.Linq;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/MultipleConditions")]
    public class MultipleConditions : Condition
    {
        public bool isAny;
        public Condition[] conditions = new Condition[2];

        public override bool IsValid(object args)
        {
            if (isAny)
                return conditions.Any(a => a.IsValid(args));

            return conditions.All(a => a.IsValid(args));
        }
    }
}