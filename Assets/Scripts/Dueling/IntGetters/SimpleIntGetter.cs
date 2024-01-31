using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/SimpleIntGetter")]
    public class SimpleIntGetter : IntGetter
    {
        public override int GetValue(object args)
        {
            return int.Parse(name);
        }
    }
}