﻿using System.Data.SQLite;
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

        var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", null);

        var hourAngle = PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, lon, ra);

        var result = PA.CoordinateSystems.EquatorialToHorizon(
            lat, 
            new(hourAngle, dec));

        return result.Altitude > 0.0;
    }

}