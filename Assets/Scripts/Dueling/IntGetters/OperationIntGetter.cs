using BBG.Numbers;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/OperationIntGetter")]
    public class OperationIntGetter : IntGetter
    {
        public Operation operation;
        public IntGetter[] intGetters = new IntGetter[2];

        public override int GetValue(object args)
        {
            int final = intGetters[0].GetValue(args);
            for (int i = 1; i < intGetters.Length; i++)
            {
                var value = intGetters[i].GetValue(args);   
                final = operation.GetResult(final, value);
            }
            return final;
        }
    }
}