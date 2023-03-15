using static System.Math;
using static CelestialMapper.Common.MathHelper;

namespace CelestialMapper.Core;

public static class CoordinatesConverter
{

    public static readonly DateTime J2000 = new DateTime(2000, 1, 1);

    public static HorizonCoordinates EquatorialToHorizonCoordinates(EquatorialCoordinates equatorialCoordinates, DateTime date, GeographicCoordinates geographicCoordinates)
    {
        var (dec, ra) = equatorialCoordinates;
        var (latitude, longitude) = geographicCoordinates;

        double daysFromJ2000 = (date - J2000).TotalDays;

        double lst = (100.46 + 0.985647 * daysFromJ2000 + longitude + 15 * date.TimeOfDay.TotalHours) + 360.0;

        double ha = lst - ra;
        if (ha < 0)
        {
            ha += 360.0;
        }

        double dec_rad = DegreesToRadians(dec),
            lat_rad = DegreesToRadians(latitude),
            ha_rad = DegreesToRadians(ha);

        double sin_alt = Sin(dec_rad) * Sin(lat_rad) + Cos(dec_rad) * Cos(lat_rad) * Cos(ha_rad);
        double alt = Asin(sin_alt);

        double cos_az = (Sin(dec_rad) - Sin(alt) * Sin(lat_rad)) / (Cos(alt) * Cos(lat_rad));
        double az = RadiansToDegrees(Acos(cos_az));

        if (Sin(ha_rad) >= 0.0)
            az = 360.0 - az;

        return new(RadiansToDegrees(alt), az);
    }

    public static CartesianCoordinates HorizonToCartesianCoordinates(double r, HorizonCoordinates horizonCoords)
    {
        var az = horizonCoords.Azimuth;

        var x = r * CosD(az);
        var y = r * CosD(az);

        return new(x, y);
    }


}
