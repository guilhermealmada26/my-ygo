using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InspectorNavigator : EditorWindow
{
    private List<Object> lastSelected = new List<Object>(maxFallback);
    private int current;

    private const int maxFallback = 20;

    [MenuItem("Window/Inspector Navigator")]
    private static void Init()
    {
        var window = GetWindow(typeof(InspectorNavigator));
        window.minSize = new Vector2(100, 30);
        window.Show();
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectedChanged;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectedChanged;
    }

    private void OnSelectedChanged()
    {
        var obj = Selection.activeObject;

        if (obj != null &&
            obj is Object &&
            obj != Current &&
            !lastSelected.Contains(obj))
        {
            ClampLast();
            lastSelected.Add(obj);
            current = lastSelected.Count - 1;
        }
    }

    private void ClampLast()
    {
        if (lastSelected.Count >= maxFallback)
        {
            //remove first
            lastSelected.RemoveAt(0);
        }
    }

    private Object Current
    {
        get
        {
            if (current >= lastSelected.Count)
                return null;
            return lastSelected[current];
        }
    }

    private void Previous()
    {
        current--;
        if (current < 0)
            current = 0;
        SetCurrent();
    }

    private void Next()
    {
        current++;
        if (current >= lastSelected.Count)
            current = lastSelected.Count - 1;
        SetCurrent();
    }

    private void SetCurrent()
    {
        var current = Current;
        if (current != null && Selection.activeObject != current)
        {
            Selection.activeObject = current;
        }
    }

    public void Clear()
    {
        lastSelected.Clear();
    }

    /**GUI***/
    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Previous"))
        {
            Previous();
        }
        if (GUILayout.Button("Next"))
        {
            Next();
        }
        GUILayout.EndHorizontal();
    }
}
