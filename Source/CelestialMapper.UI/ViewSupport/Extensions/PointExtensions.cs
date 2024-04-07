namespace CelestialMapper.UI;

using System.Windows;

public static class PointExtensions
{

    public static Point ToPoint(this (double, double) tuple)
    {
        return new(tuple.Item1, tuple.Item2);
    }

    public static Point Add(this Point point, double value)
    {
        return new(point.X + value, point.Y + value);
    }

}
