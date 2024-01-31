using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class SOUtility
{
    public static List<T> GetAll<T>(string baseFolder) where T : ScriptableObject
    {
        var res = new List<T>();
        string typeName = typeof(T).FullName;
        if (!Directory.Exists(baseFolder))
        {
            Directory.CreateDirectory(baseFolder);
            AssetDatabase.Refresh();
        }

        string[] guids = AssetDatabase.FindAssets("t:" + typeName, new string[] { baseFolder });
        foreach (string guid in guids)
            res.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)));
        return res;
    }

    public static void Rename(ScriptableObject so, string newName)
    {
        if (so == null || so.name == newName)
            return;
        var assetPath = AssetDatabase.GetAssetPath(so);
        var newAssetPath = assetPath.Replace(so.name, newName);
        if (AssetDatabase.LoadAssetAtPath<ScriptableObject>(newAssetPath))
        {
            Debug.LogWarning($"File with name: {newName} already exists.");
            return;
        }

        AssetDatabase.RenameAsset(assetPath, newName);
        AssetDatabase.MoveAsset(assetPath, newAssetPath);
        so.name = newName;
    }

    public static ScriptableObject DuplicateAsset(ScriptableObject so)
    {
        string assetPath = AssetDatabase.GetAssetPath(so);
        var newAssetPath = assetPath.Replace(so.name, so.name + "(Clone)");
        AssetDatabase.CopyAsset(assetPath, newAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return AssetDatabase.LoadAssetAtPath<ScriptableObject>(newAssetPath);
    }

    public static void DeleteAsset(ScriptableObject so)
    {
        string assetPath = AssetDatabase.GetAssetPath(so);
        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
