namespace CelestialMapper.ViewModel;

public static class CelestialObjectHelper
{

    #region Size

    private static readonly NumRange<double> bigObject = NumRange.Of(double.MinValue, -1d);
    private static readonly NumRange<double> mediumObject = NumRange.Of(-1d, 1d);
    private static readonly NumRange<double> smallObject = NumRange.Of(1d, 4d);
    private static readonly NumRange<double> verySmallObject = NumRange.Of(4, double.MaxValue);

    public static double GetSizeBasedOnMagnitude(double magnitude)
    {
        double objectDiameter = magnitude switch
        {
            var m when verySmallObject.InRange(m, NumRangeKind.Exclusive) => 0.25,
            var m when smallObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 0.5,
            var m when mediumObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 1,
            var m when bigObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 2,
            _ => throw null!
        } * 6;

        return objectDiameter;
    }

    #endregion

}
