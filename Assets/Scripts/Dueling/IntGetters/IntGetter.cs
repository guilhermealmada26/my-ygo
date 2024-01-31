using UnityEngine;

namespace BBG.ValueGetters
{
    public abstract class IntGetter : ScriptableObject
    {
        public abstract int GetValue(object args);
    }
}