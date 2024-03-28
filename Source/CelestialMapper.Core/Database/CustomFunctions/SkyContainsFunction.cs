using System.Data.SQLite;
using System.Globalization;
using CelestialMapper.Common;

namespace CelestialMapper.Core.Database.CustomFunctions;

[SQLiteFunction(Name = "skycontains", Arguments = 5, FuncType = FunctionType.Scalar)]
internal class SkyContainsFunction : SQLiteFunction
{

    public override object Invoke(object[] args)
    {
        var ra = args.As<double>(0);
        var dec = args.As<double>(1);
        var date = args.As<string>(2);
        var lat = args.As<double>(3);
        var lon = args.As<double>(4);

        var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        var hourAngle = PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, lon, ra * 15d);

        return AstronomyCoordsHelper.SkyContains(new(hourAngle, dec), new(lat, lon));
    }

}
