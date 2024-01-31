using System;
using System.Collections.Generic;
using System.Linq;

public static class DuplicateCounter<T>
{
    public static Dictionary<T, int> TrackDuplicates(IEnumerable<T> enumerable, Func<T, object> comparisonValue)
    {
        Dictionary<T, int> duplicateTracker = new();

        foreach (T item in enumerable)
        {
            bool itemFound = false;
            foreach (var trackerItem in duplicateTracker)
            {
                if (comparisonValue(item).Equals(comparisonValue(trackerItem.Key)))
                {
                    duplicateTracker[trackerItem.Key]++;
                    itemFound = true;
                    break;
                }
            }
            if (!itemFound)
            {
                duplicateTracker[item] = 1;
            }
        }

        return duplicateTracker;
    }

    public static string GetDuplicatesString(IEnumerable<T> enumerable, Func<T, object> comparisonValue)
    {
        var str = string.Empty;
        var duplicates = TrackDuplicates(enumerable, comparisonValue);
        foreach(var duplicate in duplicates)
        {
            str += $"{duplicate.Value} {comparisonValue(duplicate.Key)}\n";
        }
        return str;
    }
}
