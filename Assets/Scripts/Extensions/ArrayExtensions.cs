using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
    public static int IndexOf<T>(this T[] array, T element)
    {
        var index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(element))
                index = i;
        }
        return index;
    }

    public static void Foreach<T>(this IEnumerable<T> items, Action<T> callback)
    {
        if(items == null) return;
        foreach (var item in items) 
            if(item != null && callback != null)
                callback(item);
    }
}
