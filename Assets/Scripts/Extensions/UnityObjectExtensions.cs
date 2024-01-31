using UnityEngine;

public static class UnityObjectExtensions
{
    public static T[] CloneAll<T>(this T[] sos) where T : Object
    {
        var clones = new T[sos.Length];
        for (int i = 0; i < clones.Length; i++)
        {
            clones[i] = Object.Instantiate(sos[i]);
            clones[i].name = sos[i].name;
        }
        return clones;
    }

    public static void SetLayer(this GameObject go, int layer)
    {
        go.layer = layer;

        //set child layer recursively
        var children = go.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }
}
