namespace CelestialMapper.Core;

using CelestialMapper.Common;
using PracticalAstronomy.CSharp;
using static PracticalAstronomy.Units;

public static class AstronomyCoordsHelper
{

    public static (double X, double Y) MapCartesianCoords(Horizon horizon, double mapDiameter)
    {
        var (az, alt) = horizon;

        const double maxAltitude = 90d;
        double radius = (maxAltitude - alt) / maxAltitude * (mapDiameter / 2.0);

        var x = radius * MathHelper.SinD(az);
        var y = radius * MathHelper.CosD(az);

        return (x, y);
    }

    public static bool SkyContains(EquatorialHourAngle eq, Geographic location)
    {
        var (lat, _) = location;
        var result = PA.CoordinateSystems.EquatorialToHorizon(
            lat,
            eq);

        return result.Altitude > 0.0;
    }

}