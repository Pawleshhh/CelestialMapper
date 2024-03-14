namespace CelestialMapper.Core;

using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

public static class AstronomyCoordsHelper
{

    public static (double X, double Y) MapCartesianCoords(Horizon horizon, double mapDiameter, double objectSize)
    {
        var (az, alt) = horizon;

        const double maxAltitude = 90d;
        double radius = (maxAltitude - alt) / maxAltitude * (mapDiameter / 2.0);

        var halfSize = objectSize / 2d;
        var x = radius * MathHelper.CosD(az);
        var y = radius * MathHelper.SinD(az);

        return (x, y);
    }

}