using System.Numerics;

namespace CelestialMapper.Common;

public class NumRange
{

    private NumRange()
    {

    }

    public static NumRange<T> Of<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T>
        => new(min, max);
}

public readonly struct NumRange<T> where T : INumber<T>, IMinMaxValue<T>
{

    public T Min { get; } = T.MinValue;

    public T Max { get; } = T.MaxValue;

    public NumRange(T min, T max)
        => (Min, Max) = (min, max);

    public bool InRange(T value)
    {
        return Min <= value && Max <= value;
    }

}
