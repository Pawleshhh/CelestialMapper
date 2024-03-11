namespace CelestialMapper.Common;

using static CelestialMapper.Common.UtilsHelper;
using System.Collections.ObjectModel;

public static partial class ArrayExtension
{

    public static T As<T>(this object[] array, int index)
        => (T)array[index];

    public static T[] Clear<T>(this T[] array)
    {
        Array.Clear(array);
        return array;
    }

    public static T[] Clear<T>(this T[] array, int index, int length)
    {
        Array.Clear(array, index, length);
        return array;
    }

    public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        => Array.AsReadOnly(array);

    public static int BinarySearch<T>(this T[] array, T value)
        => Array.BinarySearch(array, value);

    public static int BinarySearch<T>(this T[] array, T value, IComparer<T> comparer)
        => Array.BinarySearch(array, value, comparer);

    public static int BinarySearch<T>(this T[] array, int index, int length, T value)
        => Array.BinarySearch(array, index, length, value);

    public static T[] Initialize<T>(int length, Func<int, T> init)
    {
        T[] array = new T[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = init(i);
        }
        return array;
    }

    public static T[] Fill<T>(this T[] array, T value)
    {
        Array.Fill(array, value);
        return array;
    }

    public static T[] Fill<T>(this T[] array, T value, int startIndex, int count)
    {
        Array.Fill(array, value, startIndex, count);
        return array;
    }

    public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
        => Array.ConvertAll(array, converter);

    public static T[] ConstrainedCopy<T>(this T[] array, int sourceIndex, T[] destination, int destinationIndex, int length)
    {
        Array.ConstrainedCopy(array, sourceIndex, destination, destinationIndex, length);
        return destination;
    }

    public static T[] Copy<T>(this T[] array)
    {
        T[] copy = new T[array.Length];
        Array.Copy(array, copy, array.Length);
        return copy;
    }

    public static T[] Copy<T>(this T[] array, int length)
    {
        T[] copy = new T[length];
        Array.Copy(array, copy, length);
        return copy;
    }

    public static T[] Copy<T>(this T[] array, long length)
    {
        T[] copy = new T[length];
        Array.Copy(array, copy, length);
        return copy;
    }

    public static T[] Copy<T>(this T[] array, int sourceIndex, int destinationIndex, int length)
    {
        T[] copy = new T[length];
        Array.Copy(array, sourceIndex, copy, destinationIndex, length);
        return copy;
    }

    public static T[] Copy<T>(this T[] array, long sourceIndex, long destinationIndex, long length)
    {
        T[] copy = new T[length];
        Array.Copy(array, sourceIndex, copy, destinationIndex, length);
        return copy;
    }

    public static bool Exists<T>(this T[] array, Predicate<T> match)
        => Array.Exists(array, match);

    public static T? Find<T>(this T[] array, Predicate<T> match)
        => Array.Find(array, match);

    public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        => Array.FindAll(array, match);

    public static T? FindLast<T>(this T[] array, Predicate<T> match)
        => Array.FindLast(array, match);

    public static int IndexOf<T>(this T[] array, T value)
        => Array.IndexOf(array, value);

    public static int IndexOf<T>(this T[] array, T value, int startIndex)
        => Array.IndexOf(array, value, startIndex);

    public static int IndexOf<T>(this T[] array, T value, int startIndex, int count)
        => Array.IndexOf(array, value, startIndex, count);

    public static int LastIndexOf<T>(this T[] array, T value)
        => Array.LastIndexOf(array, value);

    public static int LastIndexOf<T>(this T[] array, T value, int startIndex)
        => Array.LastIndexOf(array, value, startIndex);

    public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count)
        => Array.LastIndexOf(array, value, startIndex, count);

    public static int FindIndex<T>(this T[] array, Predicate<T> match)
        => Array.FindIndex(array, match);

    public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        => Array.FindIndex(array, startIndex, match);

    public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        => Array.FindIndex(array, startIndex, count, match);

    public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
        => Array.FindLastIndex(array, match);

    public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        => Array.FindLastIndex(array, startIndex, match);

    public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        => Array.FindLastIndex(array, startIndex, count, match);

    public static bool TryIndexOf<T>(this T[] array, T value, out int index)
        => TryGetIndexHelper(() => Array.IndexOf(array, value), out index);

    public static bool TryIndexOf<T>(this T[] array, T value, int startIndex, out int index)
        => TryGetIndexHelper(() => Array.IndexOf(array, value, startIndex), out index);

    public static bool TryIndexOf<T>(this T[] array, T value, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => Array.IndexOf(array, value, startIndex, count), out index);

    public static bool TryLastIndexOf<T>(this T[] array, T value, out int index)
        => TryGetIndexHelper(() => Array.LastIndexOf(array, value), out index);

    public static bool TryLastIndexOf<T>(this T[] array, T value, int startIndex, out int index)
        => TryGetIndexHelper(() => Array.LastIndexOf(array, value, startIndex), out index);

    public static bool TryLastIndexOf<T>(this T[] array, T value, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => Array.LastIndexOf(array, value, startIndex, count), out index);

    public static bool TryFindIndex<T>(this T[] array, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindIndex(array, match), out index);

    public static bool TryFindIndex<T>(this T[] array, int startIndex, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindIndex(array, startIndex, match), out index);

    public static bool TryFindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindIndex(array, startIndex, count, match), out index);

    public static bool TryFindLastIndex<T>(this T[] array, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindLastIndex(array, match), out index);

    public static bool TryFindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindLastIndex(array, startIndex, match), out index);

    public static bool TryFindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match, out int index)
        => TryGetIndexHelper(() => Array.FindLastIndex(array, startIndex, count, match), out index);

    public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
        => Array.TrueForAll(array, match);

    public static void ForEach<T>(this T[] array, Action<T> action)
        => Array.ForEach(array, action);

    public static T[] Resize<T>(this T[] array, int newSize)
    {
        Array.Resize(ref array, newSize);
        return array;
    }

    public static T[] ResizeAsCopy<T>(this T[] array, int newSize)
    {
        T[] copy = array.Copy();
        Array.Resize(ref copy, newSize);
        return copy;
    }

    public static T[] Reverse<T>(this T[] array)
    {
        Array.Reverse(array);
        return array;
    }

    public static T[] Reverse<T>(this T[] array, int index, int length)
    {
        Array.Reverse(array, index, length);
        return array;
    }

    public static T[] ReverseAsCopy<T>(this T[] array)
    {
        T[] copy = array.Copy();
        Array.Reverse(copy);
        return copy;
    }

    public static T[] ReverseAsCopy<T>(this T[] array, int index, int length)
    {
        T[] copy = array.Copy();
        Array.Reverse(copy, index, length);
        return copy;
    }

    public static T[] Sort<T>(this T[] array)
    {
        Array.Sort(array);
        return array;
    }

    public static T[] Sort<T>(this T[] array, Comparison<T> comparison)
    {
        Array.Sort(array, comparison);
        return array;
    }

    public static T[] Sort<T>(this T[] array, IComparer<T> comparer)
    {
        Array.Sort(array, comparer);
        return array;
    }

    public static T[] Sort<T>(this T[] array, int index, int length)
    {
        Array.Sort(array, index, length);
        return array;
    }

    public static T[] Sort<T>(this T[] array, int index, int length, IComparer<T> comparer)
    {
        Array.Sort(array, index, length, comparer);
        return array;
    }

    public static T[] SortAsCopy<T>(this T[] array)
    {
        T[] copy = array.Copy();
        Array.Sort(copy);
        return copy;
    }

    public static T[] SortAsCopy<T>(this T[] array, Comparison<T> comparison)
    {
        T[] copy = array.Copy();
        Array.Sort(copy, comparison);
        return copy;
    }

    public static T[] SortAsCopy<T>(this T[] array, IComparer<T> comparer)
    {
        T[] copy = array.Copy();
        Array.Sort(copy, comparer);
        return copy;
    }

    public static T[] SortAsCopy<T>(this T[] array, int index, int length)
    {
        T[] copy = array.Copy();
        Array.Sort(copy, index, length);
        return copy;
    }

    public static T[] SortAsCopy<T>(this T[] array, int index, int length, IComparer<T> comparer)
    {
        T[] copy = array.Copy();
        Array.Sort(copy, index, length, comparer);
        return copy;
    }

    public static T[] Create<T>(params T[] array)
        => array;

}
