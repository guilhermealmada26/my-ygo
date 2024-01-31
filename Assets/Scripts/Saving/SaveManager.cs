using UnityEngine;

namespace BBG.Saving
{
    public abstract class SaveManager : ScriptableObject
    {
        public abstract void Save(object data);

        public abstract T Load<T>();
    }
}