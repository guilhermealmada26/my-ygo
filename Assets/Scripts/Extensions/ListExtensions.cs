using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    static readonly Random rnd;

    static ListExtensions()
    {
        rnd = new Random();
    }

    /// <summary>
    /// Extension method that will shuffle the list elements.
    /// </summary>
    public static void Shuffle<T>(this IList<T> elements, Random random = null)
    {
        int n = elements.Count;
        random ??= rnd;

        while (n > 1)
        {
            n--;
            int k = random.Next(0, n + 1);
            var temp = elements[n];
            elements[n] = elements[k];
            elements[k] = temp;
        }
    }

    public static T Pop<T>(this IList<T> list)
    {
        if (list.Count == 0)
            return default;

        var index = list.Count - 1;
        var result = list[index];
        list.RemoveAt(index);
        return result;
    }

    public static IList<T> Pop<T>(this IList<T> list, int count)
    {
        var resultCount = count < list.Count ? count : list.Count;
        var result = new List<T>(resultCount);

        for (int i = 0; i < resultCount; ++i)
        {
            T item = list.Pop();
            result.Add(item);
        }
        return result;
    }

    public static T Last<T>(this IList<T> list)
    {
        if (list.Count == 0)
            return default;
        return list[list.Count - 1];
    }

    public static T GetRandom<T>(this IList<T> list)
    {
        int random = UnityEngine.Random.Range(0, list.Count);
        return list[random];
    }

    public static T GetRandom<T>(this IList<T> list, System.Random random)
    {
        int index = random.Next(0, list.Count);
        return list[index];
    }

    public static int GetCount<T>(this IList<T> list, T item)
    {
        int count = 0;
        foreach (var it in list)
        {
            if (it.Equals(item))
                count++;
        }
        return count;
    }

    public static int GetCountWhere<T>(this IList<T> list, Predicate<T> predicade)
    {
        int count = 0;
        foreach (var it in list)
        {
            if (predicade(it))
                count++;
        }
        return count;
    }

    /// <summary>
    /// Calculates the percentage of elements on the list, related to the list to check given.
    /// </summary>
    /// <returns>Value between 0 and 1 (-1 for error)</returns>
    public static float PercentageCompleted<T>(this IList<T> list, IEnumerable<T> listToCheck)
    {
        if (listToCheck == null || !listToCheck.Any())
            return 1;
        if (list == null || !list.Any())
            return -1;

        var malletWithoutCopies = list.Distinct().ToArray();
        var cardsWithoutCopies = listToCheck.Distinct().ToArray();
        var intersect = malletWithoutCopies.Intersect(cardsWithoutCopies).ToArray();
        var percent = (float)intersect.Length / cardsWithoutCopies.Length;

        return percent;
    }


    /// <summary>
    /// Removes all the references where the predicate returns false.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicade"></param>
    /// <returns></returns>
    public static void Filter<T>(this IList<T> list, Predicate<T> predicade)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var item = list[i];
            if (!predicade.Invoke(item))
            {
                list.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Adds only the items not contained in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="toAdd"></param>
    /// <returns></returns>
    public static IList<T> AddNonContained<T>(this IList<T> list, IEnumerable<T> toAdd)
    {
        foreach (var item in toAdd)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
        return list;
    }
}