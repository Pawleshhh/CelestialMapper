namespace CelestialMapper.ViewModel;

public static class CelestialObjectHelper
{

    #region Size

    private static readonly NumRange<double> bigObject = NumRange.Of(double.MinValue, -1d);
    private static readonly NumRange<double> mediumObject = NumRange.Of(-1d, 1d);
    private static readonly NumRange<double> smallObject = NumRange.Of(1d, 4d);
    private static readonly NumRange<double> verySmallObject = NumRange.Of(4, double.MaxValue);

    private const double verySmallSize = 0.25;
    private const double smallSize = 0.5;
    private const double mediumSize = 1;
    private const double bigSize = 2;

    public static double GetSizeBasedOnMagnitude(double magnitude)
    {
        double objectDiameter = magnitude switch
        {
            var m when verySmallObject.InRange(m, NumRangeKind.Exclusive) => verySmallSize,
            var m when smallObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => smallSize,
            var m when mediumObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => mediumSize,
            var m when bigObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => bigSize,
            _ => throw null!
        } * 6;

        return objectDiameter;
    }

    public static double GetMagnitudeBasedOnSize(double size)
    {
        double objectDiameter = size switch
        {
            <= verySmallSize => verySmallSize,
            <= smallSize => smallSize,
            <= mediumSize => mediumSize,
            <= bigSize => bigSize,
            _ => throw null!
        } / 6;

        return objectDiameter;
    }

    #endregion

}
