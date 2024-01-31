using UnityEditor;
using UnityEngine;

public class AssetReferencePrinter : EditorWindow
{
    private Object selectedAsset;
    private string[] referencedAssets;

    [MenuItem("Tools/Asset Reference Printer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetReferencePrinter));
    }

    private void OnGUI()
    {
        GUILayout.Label("Select an asset to print its references:", EditorStyles.boldLabel);
        selectedAsset = EditorGUILayout.ObjectField(selectedAsset, typeof(Object), true);

        if (GUILayout.Button("Print References"))
        {
            PrintReferences();
        }

        if (referencedAssets != null && referencedAssets.Length > 0)
        {
            GUILayout.Label($"Referenced Assets: ({referencedAssets.Length})", EditorStyles.boldLabel);
            var str = "";
            for (int i = 0; i < referencedAssets.Length; i++)
            {
                str += $"{referencedAssets[i]}\n";
            }

            GUILayout.Label(str);
        }
    }

    private void PrintReferences()
    {
        if (selectedAsset == null)
        {
            UnityEngine.Debug.LogWarning("No asset selected.");
            return;
        }

        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        string selectedAssetPath = AssetDatabase.GetAssetPath(selectedAsset);

        System.Collections.Generic.List<string> referencedAssetNames = new System.Collections.Generic.List<string>();

        foreach (string assetPath in allAssetPaths)
        {
            string[] dependencies = AssetDatabase.GetDependencies(assetPath, false);
            if (ArrayContains(dependencies, selectedAssetPath))
            {
                string assetName = AssetDatabase.LoadAssetAtPath<Object>(assetPath).name;
                referencedAssetNames.Add(assetName);
            }
        }

        referencedAssets = referencedAssetNames.ToArray();
    }

    private bool ArrayContains(string[] array, string item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == item)
            {
                return true;
            }
        }
        return false;
    }
}
