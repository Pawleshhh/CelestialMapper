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

    public NumRange()
        => (Min, Max) = (T.MinValue, T.MaxValue);

    public NumRange(T min, T max)
        => (Min, Max) = (min, max);

    public bool InRange(T value)
    {
        return Min <= value && value <= Max;
    }

    public bool InRange(T value, NumRangeKind bothSidesKind)
    {
        if (bothSidesKind == NumRangeKind.Inclusive)
        {
            return InRange(value);
        }

        return Min < value && value < Max;
    }

    public bool InRange(T value, NumRangeKind minKind, NumRangeKind maxKind)
    {
        return (minKind, maxKind) switch
        {
            (NumRangeKind.Inclusive, NumRangeKind.Exclusive) => Min <= value && value < Max,
            (NumRangeKind.Exclusive, NumRangeKind.Inclusive) => Min < value && value <= Max,
            _ => InRange(value, minKind) // both kinds must be equal there
        };
    }
}

public enum NumRangeKind
{
    Inclusive,
    Exclusive
}
