using BBG.Encryption;
using System.IO;
using UnityEngine;

namespace BBG.Saving
{
    public abstract class FileSaver : SaveManager
    {
        public EncryptionType encryptionType;
        public FolderName folderName;
        public string fileName, fileExtension;
        public bool logSave;

        public override T Load<T>()
        {
            var encryptor = encryptionType.GetEncryptor();
            var path = folderName.GetName(fileName, fileExtension);
            if (!File.Exists(path))
                return default(T);

            var text = File.ReadAllText(path);
            text = encryptor.Decrypt(text);
            if (logSave)
                Debug.LogWarning("File loaded from: " + path);
            return Deserialize<T>(text);
        }

        public override void Save(object data)
        {
            var encryptor = encryptionType.GetEncryptor();
            var path = folderName.GetName(fileName, fileExtension);
            var text = Serialize(data);
            text = encryptor.Encrypt(text);
            File.WriteAllText(path, text);
            if (logSave)
                Debug.LogWarning("File saved at: " + path);
        }

        protected abstract string Serialize(object data);

        protected abstract T Deserialize<T>(string data);
    }
}