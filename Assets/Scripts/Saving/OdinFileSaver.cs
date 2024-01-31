using Sirenix.Serialization;
using System.IO;
using UnityEngine;

namespace BBG.Saving
{
    [CreateAssetMenu(menuName = "Saving/OdinFileSaver")]
    public class OdinFileSaver : SaveManager
    {
        public FolderName folderName;
        public string fileName, fileExtension;
        public DataFormat format;
        public bool logSave;

        public override T Load<T>()
        {
            var path = folderName.GetName(fileName, fileExtension);
            if (!File.Exists(path))
                return default(T);

            var bytes = File.ReadAllBytes(path);
            if (logSave)
                Debug.LogWarning("File loaded from: " + path);
            return SerializationUtility.DeserializeValue<T>(bytes, format);
        }

        public override void Save(object data)
        {
            var path = folderName.GetName(fileName, fileExtension);
            var bytes = SerializationUtility.SerializeValue(data, format);
            File.WriteAllBytes(path, bytes);
            if (logSave)
                Debug.LogWarning("File saved at: " + path);
        }
    }
}