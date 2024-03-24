namespace CelestialMapper.Common;

public static class MathHelper
{
    public static double SinD(double a)
        => Math.Sin(DegreesToRadians(a));

    public static double CosD(double a)
        => Math.Cos(DegreesToRadians(a));

    public static double TanD(double a)
        => Math.Tan(DegreesToRadians(a));

    public static double AsinD(double a)
        => RadiansToDegrees(Math.Asin(a));

    public static double AcosD(double a)
        => RadiansToDegrees(Math.Acos(a));

    public static double AtanD(double a)
        => RadiansToDegrees(Math.Atan(a));

    public static double DegreesToRadians(double degrees)
        => degrees * Math.PI / 180.0;

    public static double RadiansToDegrees(double radians)
        => radians * 180.0 / Math.PI;

    public static (double X, double Y) PointOnLine(
        double x1, double y1,
        double x2, double y2,
        double t)
    {
        double tx = (1 - t) * x1 + t * x2;
        double ty = (1 - t) * y1 + t * y2;

        return (tx, ty);
    }

    public static double LineLength(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(
            Math.Pow(x2 - x1, 2) +
            Math.Pow(y2 - y1, 2));
    }
}
