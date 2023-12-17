using System.Numerics;

namespace CelestialMapper.Common;

public struct NumRange
{
    public static NumRange<T> Of<T>(T min, T max) where T : INumber<T>
        => new(min, max);
}

public readonly struct NumRange<T> where T : INumber<T>
{

    public T Min { get; }

    public T Max { get; }

    public NumRange(T min, T max)
        => (Min, Max) = (min, max);

}
