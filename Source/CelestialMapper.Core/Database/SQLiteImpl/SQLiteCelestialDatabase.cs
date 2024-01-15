using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database.CustomFunctions;
using CelestialMapper.Core.Infrastructure.Map;
using PracticalAstronomy.CSharp;
using System.Data.SQLite;
using CSharpUtilities;

namespace CelestialMapper.Core.Database.SQLiteImpl;

public class SQLiteCelestialDatabase : ICelestialDatabase
{

    #region Fields

    private readonly Func<int> getProcessorCount;

    private readonly IDatabaseWrapper dbWrapper;
    private readonly SQLiteConnectionStringBuilder connectionBuilder;

    private const int ParallelThreshold = 10_000;

    #endregion

    #region Constructors

    static SQLiteCelestialDatabase()
    {
        SQLiteFunction.RegisterFunction(typeof(SkyContainsFunction));
    }

    public SQLiteCelestialDatabase(IDatabaseWrapper dbWrapper, Func<int>? getProcessorCount = null)
    {
        this.getProcessorCount = getProcessorCount ?? (() => Environment.ProcessorCount);
        this.dbWrapper = dbWrapper ?? throw new ArgumentNullException(nameof(dbWrapper));
        this.connectionBuilder = CreateSQLiteConnectionStringBuilder();
    }

    #endregion

    #region Methods

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime)
    {
        return GetCelestialObjects(location, dateTime, MapConstants.DefaultMagnitudeRange);
    }

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime, NumRange<double> magnitudeRange)
    {
        using var connection = this.dbWrapper.CreateDbConnection(this.connectionBuilder);
        connection.Open();

        string query =
            $"SELECT * " +
            $"FROM stars " +
            $"WHERE {DbColumnNames.StarsColumnNames.Magnitude} BETWEEN {magnitudeRange.Min} AND {magnitudeRange.Max}" +
            $"AND (90 - {location.Latitude} + {DbColumnNames.StarsColumnNames.Declination}) >= 0";

        var rows = this.dbWrapper.Query<StarDataRow>(connection, query);

        List<CelestialObject> celestialObjects = new();
        void MapObject(StarDataRow row)
            => celestialObjects.Add(CelestialObject.FromStarDataRow(location, dateTime, row));

        if (rows.Count() <= ParallelThreshold || this.getProcessorCount() < 2)
        {
            rows.ForEach(MapObject);
        }
        else
        {
            Parallel.ForEach(rows, MapObject);
        }

        connection.Close();
        
        return celestialObjects;
    }

    #endregion

    #region Private methods

    private static SQLiteConnectionStringBuilder CreateSQLiteConnectionStringBuilder()
    {
        return new()
        {
            DataSource = "Data Source=stars.sqlite"
        };
    }

    #endregion

}
