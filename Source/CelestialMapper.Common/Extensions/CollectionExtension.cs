using System.Collections.ObjectModel;

namespace CelestialMapper.Common;

public static class CollectionExtension
{

    public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    public static void Reset<T>(this Collection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        collection.AddRange(items);
    }

}
