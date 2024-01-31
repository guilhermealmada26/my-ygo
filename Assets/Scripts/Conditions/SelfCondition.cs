using BBG.Dueling.Effects;
using UnityEngine;

namespace BBG.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/Cards/SelfCondition")]
    public class SelfCondition : Condition
    {
        public override bool IsValid(object args)
        {
            if (args is FilterArgs filter)
                return filter.target == filter.caller.card;
            return false;
        }
    }
}