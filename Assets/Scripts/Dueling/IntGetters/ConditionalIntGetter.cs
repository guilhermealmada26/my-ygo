using BBG.Conditions;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/ConditionalIntGetter")]
    public class ConditionalIntGetter : IntGetter
    {
        [System.Serializable]
        public class Pair
        {
            public Condition condition;
            public IntGetter intGetter;
        }

        public IntGetter defaultValue;
        public Pair[] conditional;

        public override int GetValue(object args)
        {
            var valueGetter = defaultValue;
            foreach(var pair in conditional)
                if(pair.condition.IsValid(args))
                    valueGetter = pair.intGetter;
            return valueGetter.GetValue(args);
        }
    }
}