namespace CelestialMapper.Core.Astronomy;

using CelestialMapper.Common;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;

public record CelestialObject(
    long Id,
    string Name, 
    Horizon HorizonCoordinates, 
    double Magnitude)
{

    public static CelestialObject FromStarDataRow(Geographic location, DateTime dateTime, StarDataRow data)
    {
        dateTime = dateTime.ToUniversalTime();
        var name = NullHelper.FirstNotNull(data.Proper, data.Bf, data.Gl, data.Hr, data.Hd, data.Hip);
        var hourAngle = PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, location.Longitude, data.Ra * 15d);
        var horizon = PA.CoordinateSystems.EquatorialToHorizon(location.Latitude, new(hourAngle, data.Dec));

        return new(data.Id, name, horizon, data.Mag);
    }

}