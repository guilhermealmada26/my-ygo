using UnityEngine;

namespace BBG.Saving
{
    public abstract class SaveDataHolder<T> : MonoBehaviour
    {
        [SerializeField] SaveManager saveManager;
        [SerializeField] bool loadOnAwake, saveOnDestroy;

        public T SavedData { protected set; get; }

        protected virtual void Awake()
        {
            if (saveManager != null && loadOnAwake && SavedData == null)
                Load();
        }

        protected virtual void OnDestroy()
        {
            if (saveOnDestroy)
                Save();
        }

        public void Save()
        {
            BeforeSave();
            saveManager.Save(SavedData);
        }

        public void Load()
        {
            SavedData = saveManager.Load<T>();

            if (SavedData == null)
            {
                SavedData = GetDefaultData();
            }

            OnLoad(SavedData);
        }

        protected virtual void OnLoad(T data) { }

        protected virtual void BeforeSave() { }

        protected abstract T GetDefaultData();
    }
}