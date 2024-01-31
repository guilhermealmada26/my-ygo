using UnityEngine;

namespace BBG.Saving
{
    public enum FolderName
    {
        StreamingAssets, PersistentDataPath
    }

    public static class FolderNameExtension
    {
        public static string GetName(this FolderName type)
        {
            if (type == FolderName.StreamingAssets)
                return Application.streamingAssetsPath;

            return Application.persistentDataPath;
        }

        public static string GetName(this FolderName type, string fileName, string fileExtension)
        {
            return GetName(type) + "/" + fileName + "." + fileExtension;
        }
    }
}