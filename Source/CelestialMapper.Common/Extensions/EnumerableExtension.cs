namespace CelestialMapper.Common;

public static class EnumerableExtension
{

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        => !enumerable?.Any() ?? true;

    public static bool IsNotNullAndExactCount<T>(this IEnumerable<T>? enumerable, int count)
        => enumerable != null && enumerable.Count() == count;

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach(var item in enumerable)
        {
            action(item);
        }
    }

    public static void Iterate<T>(this IEnumerable<T> enumerable, Action<T, int> iterate)
    {
        var enumerator = enumerable.GetEnumerator();
        int i = 0;
        do
        {
            iterate(enumerator.Current, i++);
        } while(enumerator.MoveNext());
    }

    public static bool AllIterate<T>(this IEnumerable<T> enumerable, Func<T, int, bool> match)
    {
        var enumerator = enumerable.GetEnumerator();
        int i = 0;
        do
        {
            if (!match(enumerator.Current, i++))
            {
                return false;
            }
        } while (enumerator.MoveNext());

        return true;
    }

}
