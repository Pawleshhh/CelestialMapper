using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using CelestialMapper.Core.Infrastructure.Map;
using  PracticalAstronomy.CSharp;
using System.Data.SQLite;
using System.Globalization;

namespace CelestialMapper.Core;

internal class SQLiteCelestialObjectHelper : SQLiteHelperBase
{

    public SQLiteCelestialObjectHelper(
        IDatabaseWrapper dbWrapper,
        ICelestialObjectProcessor celestialObjectProcessor,
        SQLiteConnectionStringBuilder connectionBuilder,
        int parallelThreshold,
        Func<int> getProcessorCount)
        : base(dbWrapper, celestialObjectProcessor, connectionBuilder, parallelThreshold, getProcessorCount)
    {

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

}
