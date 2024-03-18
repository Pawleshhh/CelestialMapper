using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using CelestialMapper.Core.Infrastructure.Map;
using  PracticalAstronomy.CSharp;
using System.Data.SQLite;
using System.Globalization;

namespace CelestialMapper.Core;

internal class SQLiteCelestialObjectHelper
{

    #region Fields

    private readonly Func<int> getProcessorCount;

    private readonly IDatabaseWrapper dbWrapper;
    private readonly ICelestialObjectProcessor celestialObjectProcessor;
    private readonly SQLiteConnectionStringBuilder connectionBuilder;

    private readonly int parallelThreshold = 10_000;

    #endregion

    public SQLiteCelestialObjectHelper(
        IDatabaseWrapper dbWrapper,
        ICelestialObjectProcessor celestialObjectProcessor,
        SQLiteConnectionStringBuilder connectionBuilder,
        int parallelThreshold,
        Func<int> getProcessorCount)
    {
        this.dbWrapper = dbWrapper;
        this.celestialObjectProcessor = celestialObjectProcessor;
        this.connectionBuilder = connectionBuilder;
        this.parallelThreshold = parallelThreshold;
        this.getProcessorCount = getProcessorCount;
    }

    public  IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime)
    {
        return GetCelestialObjects(location, dateTime, MapConstants.DefaultMagnitudeRange);
    }

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime, NumRange<double> magnitudeRange)
    {
        using var connection = this.dbWrapper.CreateDbConnection(this.connectionBuilder);
        connection.Open();

        string query =
            $"SELECT * " +
            $"FROM {DbColumnNames.StarsColumnNames.TableName} " +
            $"WHERE {MagnitudeCondition(magnitudeRange)} " +
            $"AND {AboveHorizonCondition(location)} " +
            $"AND {SkyContainsCondition(location, dateTime)}";

        var rows = this.dbWrapper.Query<StarDataRow>(connection, query);

        List<CelestialObject> celestialObjects = new();
        void MapObject(StarDataRow row)
            => celestialObjects.Add(CelestialObject.FromStarDataRow(location, dateTime, row));

        bool parallelProcessing =
            rows.Count() > this.parallelThreshold
            && this.getProcessorCount() >= 2;

        this.celestialObjectProcessor.Process(rows, MapObject, parallelProcessing);

        connection.Close();

        return celestialObjects;
    }

    #region Helpers

    private static string MagnitudeCondition(NumRange<double> magnitude)
    {
        return $"{DbColumnNames.StarsColumnNames.Magnitude} BETWEEN {magnitude.Min} AND {magnitude.Max}";
    }

    private static string AboveHorizonCondition(Geographic location)
    {
        return $"(90 - {FormatDouble(location.Latitude)} + {DbColumnNames.StarsColumnNames.Declination}) >= 0";
    }

    private static string SkyContainsCondition(Geographic location, DateTime dateTime)
    {
        return $"SKYCONTAINS({DbColumnNames.StarsColumnNames.RightAcension}, {DbColumnNames.StarsColumnNames.Declination}, " +
            $"'{dateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}', " +
            $"{FormatDouble(location.Latitude)}, " +
            $"{FormatDouble(location.Longitude)})";
    }

    private static string FormatDouble(double value)
    {
        return value.ToString("N6", CultureInfo.InvariantCulture);
    }

    #endregion

}
