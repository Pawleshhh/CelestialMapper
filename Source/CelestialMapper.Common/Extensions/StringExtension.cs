namespace CelestialMapper.Common;

using static CelestialMapper.Common.UtilsHelper;

public static class StringExtension
{

    public static bool TryIndexOf(this string @string, string value, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value), out index);

    public static bool IndexOf(this string @string, string value, int startIndex, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value, startIndex), out index);

    public static bool IndexOf(this string @string, string value, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value, startIndex, count), out index);

    public static bool IndexOf(this string @string, string value, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value, comparisonType), out index);

    public static bool IndexOf(this string @string, string value, int startIndex, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value, startIndex, comparisonType), out index);

    public static bool IndexOf(this string @string, string value, int startIndex, int count, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.IndexOf(value, startIndex, count, comparisonType), out index);

    public static bool LastIndexOf(this string @string, char value, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value), out index);

    public static bool LastIndexOf(this string @string, char value, int startIndex, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex), out index);

    public static bool LastIndexOf(this string @string, char value, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex, count), out index);

    public static bool LastIndexOfAny(this string @string, char[] anyOf, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOfAny(anyOf), out index);

    public static bool LastIndexOfAny(this string @string, char[] anyOf, int startIndex, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOfAny(anyOf, startIndex), out index);

    public static bool LastIndexOfAny(this string @string, char[] anyOf, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOfAny(anyOf, startIndex, count), out index);

    public static bool LastIndexOf(this string @string, string value, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value), out index);

    public static bool LastIndexOf(this string @string, string value, int startIndex, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex), out index);

    public static bool LastIndexOf(this string @string, string value, int startIndex, int count, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex, count), out index);

    public static bool LastIndexOf(this string @string, string value, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, comparisonType), out index);

    public static bool LastIndexOf(this string @string, string value, int startIndex, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex, comparisonType), out index);

    public static bool LastIndexOf(this string @string, string value, int startIndex, int count, StringComparison comparisonType, out int index)
        => TryGetIndexHelper(() => @string.LastIndexOf(value, startIndex, count, comparisonType), out index);

    public static bool IsNullOrEmpty(this string? @string)
        => string.IsNullOrEmpty(@string);

    public static bool IsNullOrWhiteSpace(this string? @string)
        => string.IsNullOrWhiteSpace(@string);

}
