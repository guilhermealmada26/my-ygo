using BBG.Dueling;
using UnityEngine;

namespace BBG.ValueGetters
{
    [CreateAssetMenu(menuName = "ValueGetters/TurnCountGetter")]
    public class TurnCountGetter : IntGetter
    {
        public override int GetValue(object args)
        {
            return Duel.Current.TurnCount;
        }
    }
}