using System;
using System.Collections.Generic;
using System.Linq;
public static class TypeUtility
{
    public static Type[] GetSubtypes(Type baseType)
    {
        var subtypes = new List<Type>();
        if (!baseType.IsAbstract)
            subtypes.Add(baseType);
        foreach (var type in baseType.Assembly.GetTypes())
            if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                subtypes.Add(type);
        return subtypes.OrderBy(t => t.Name).ToArray();
    }
}
