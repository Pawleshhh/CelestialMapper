namespace CelestialMapper.Common;

public static class NullHelper
{
    public static T FirstNotNull<T>(params T?[] values)
        where T : class
    {
        var value = FirstNotNullOrDefault(values);

        if (value is not null)
        {
            return value;
        }

        throw new InvalidOperationException("All values are null");
    }

    public static T? FirstNotNullOrDefault<T>(params T?[] values)
        where T : class
    {
        foreach (var value in values)
        {
            if (value is not null)
            {
                return value;
            }
        }

        return default;
    }
}
