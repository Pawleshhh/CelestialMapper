﻿using CelestialMapper.Common;
using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace CelestialMapper.Core;

public class DatabaseManager
{

    private static readonly string databasePath = "./stars.sqlite";
    private static readonly string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";

    public async Task<IEnumerable<CelestialObject>> GetCelestialObjects(DateTime dateTime, GeographicCoordinates geographicCoordinates, double magnitude, Func<CelestialObject, bool>? predicate = null)
    {
        using DbConnection connection = CreateConnection();
        try
        {
            connection.Open();

            using DbCommand command = connection.CreateCommand();
            var (latitude, longitude) = geographicCoordinates;
            command.CommandText = $"SELECT id, proper, ra, dec, mag FROM stars " +
                $"WHERE skycontains(ra, dec, {dateTime.ToString(dateTimeFormat)}, {latitude}, {longitude})" +
                    $"AND mag <= {magnitude}";

            using var reader = await command.ExecuteReaderAsync();
            var result = GetCelestialObjects(reader, dateTime, geographicCoordinates);

            if (predicate is null)
            {
                return await Task.FromResult(result.ToBlockingEnumerable());
            }

            return FilterCelestialObjects(result, predicate).ToBlockingEnumerable();
        }
        finally
        {
            connection.Close();
        }
    }

    private async IAsyncEnumerable<CelestialObject> GetCelestialObjects(DbDataReader reader, DateTime dateTime, GeographicCoordinates geographicCoordinates)
    {
        while (await reader.ReadAsync())
        {
            var id = await reader.GetDataOrDefault(0, reader.GetInt64);
            var name = await reader.GetDataOrDefault(1, reader.GetString);
            var equatorial = new EquatorialCoordinates(await reader.GetDataOrDefault(3, reader.GetDouble), await reader.GetDataOrDefault(2, reader.GetDouble));
            var horizon = CoordinatesConverter.EquatorialToHorizonCoordinates(equatorial, dateTime, geographicCoordinates);
            var mag = await reader.GetDataOrDefault(4, reader.GetDouble);

            yield return new(id, name ?? String.Empty, equatorial, horizon, mag);
        }
    }

    private async IAsyncEnumerable<CelestialObject> FilterCelestialObjects(IAsyncEnumerable<CelestialObject> result, Func<CelestialObject, bool> predicate)
    {
        await foreach (var celestialObject in result)
        {
            if (predicate(celestialObject))
            {
                yield return celestialObject;
            }
        }
    }

    private static SqliteConnection CreateConnection()
    {
        SqliteConnection connection = new SqliteConnection($"Data Source={databasePath}");

        connection.CreateFunction("skycontains",
            (double ra, double dec, string date, double latitude, double longitude) =>
            {
                var dateTime = DateTime.ParseExact(date, dateTimeFormat, null);

                var (alt, _) = CoordinatesConverter.EquatorialToHorizonCoordinates(new(dec, ra / 24.0 * 360.0), dateTime.ToUniversalTime(), new(latitude, longitude));

                return alt > 0.0;
            }, true);

        return connection;
    }

}
