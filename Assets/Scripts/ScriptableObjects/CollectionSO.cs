using UnityEngine;

namespace BBG.ScriptableObjects
{
    public abstract class CollectionSO<T> : ScriptableObject where T : Object
    {
        [SerializeField] protected T[] assets;

        public T[] Assets { get => assets; set => assets = value; }

        public T Get(string name)
        {
            foreach (var so in Assets)
                if (so.name == name)
                    return so;
            return null;
        }
    }
}