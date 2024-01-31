using UnityEngine;

namespace BBG.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool IsValid(object args);
    }
}