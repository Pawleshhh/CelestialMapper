using CelestialMapper.Common;
using Microsoft.Data.Sqlite;
using PracticalAstronomy.CSharp;
using System.Data.Common;
using System.Globalization;

namespace CelestialMapper.Core;

public class DatabaseManager
{

    private static readonly string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
    private readonly string databasePath = "./stars.sqlite";

    public DatabaseManager() { }

    public DatabaseManager(string path)
    {
        this.databasePath = path;
    }

    public async Task<IEnumerable<CelestialObject>> GetCelestialObjects(DateTime dateTime, Geographic geographicCoordinates, double magnitude, Func<CelestialObject, bool>? predicate = null)
    {
        using DbConnection connection = CreateConnection();
        connection.Open();

        using DbCommand command = connection.CreateCommand();
        var (latitude, longitude) = geographicCoordinates;
        command.CommandText =
            $"SELECT id, proper, ra, dec, mag FROM stars " +
            $"WHERE skycontains(ra, dec, \"{dateTime.ToString(dateTimeFormat)}\", {latitude.ToString(CultureInfo.InvariantCulture)}, {longitude.ToString(CultureInfo.InvariantCulture)}) " +
                $"AND mag <= {magnitude}";

        using var reader = await command.ExecuteReaderAsync();
        var result = GetCelestialObjects(reader, dateTime, geographicCoordinates);

        if (predicate is null)
        {
            return await Task.FromResult(result.ToBlockingEnumerable());
        }

        return FilterCelestialObjects(result, predicate).ToBlockingEnumerable();
    }

    private async IAsyncEnumerable<CelestialObject> GetCelestialObjects(DbDataReader reader, DateTime dateTime, Geographic geographic)
    {
        var (lat, lon) = geographic;

        while (await reader.ReadAsync())
        {
            var id = await reader.GetDataOrDefault(0, reader.GetInt64);
            var name = await reader.GetDataOrDefault(1, reader.GetString);

            var eq = 
                new EquatorialRightAscension(
                    await reader.GetDataOrDefault(3, reader.GetDouble),
                    await reader.GetDataOrDefault(2, reader.GetDouble));
            var (ra, dec) = eq;

            var horizon = PA.CoordinateSystems
                .EquatorialToHorizon(lat, new(PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, lon, ra), dec));

            var mag = await reader.GetDataOrDefault(4, reader.GetDouble);

            yield return new(id, name ?? String.Empty, eq, horizon, mag);
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

    private SqliteConnection CreateConnection()
    {
        SqliteConnection connection = new($"Data Source={this.databasePath}");

        connection.CreateFunction("skycontains",
            (double ra, double dec, string date, double latitude, double longitude) =>
            {
                var dateTime = DateTime.ParseExact(date, dateTimeFormat, null);

                var (alt, _) = PA.CoordinateSystems
                    .EquatorialToHorizon(
                        latitude, 
                        new(PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, longitude, ra), dec));

                return alt > 0.0;
            }, true);

        return connection;
    }

}
