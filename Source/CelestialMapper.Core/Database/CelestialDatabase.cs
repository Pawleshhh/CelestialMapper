using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database.CustomFunctions;
using CelestialMapper.Core.Infrastructure.Map;
using PracticalAstronomy.CSharp;
using System.Data.SQLite;
using Dapper;

namespace CelestialMapper.Core.Database;

public class CelestialDatabase : ICelestialDatabase
{

    #region Fields

    private readonly SQLiteConnectionStringBuilder connectionBuilder;

    private const int ParallelThreshold = 10_000;

    #endregion

    #region Constructors

    static CelestialDatabase()
    {
        SQLiteFunction.RegisterFunction(typeof(SkyContainsFunction));
    }

    public CelestialDatabase()
    {
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
        using SQLiteConnection connection = new(this.connectionBuilder.ConnectionString);
        connection.Open();

        string query =
            $"SELECT * " +
            $"FROM stars " +
            $"WHERE {DbColumnNames.StarsColumnNames.Magnitude} BETWEEN {magnitudeRange.Min} AND {magnitudeRange.Max}" +
            $"AND (90 - {location.Latitude} + {DbColumnNames.StarsColumnNames.Declination}) >= 0";

        var rows = connection.Query<StarDataRow>(query);

        List<CelestialObject> celestialObjects = new();
        void MapObject(StarDataRow row) => celestialObjects.Add(CelestialObject.FromStarDataRow(location, dateTime, row));

        if (rows.Count() <= ParallelThreshold || Environment.ProcessorCount < 2)
        {
            foreach (var row in rows)
            {
                MapObject(row);
            }
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

    private SQLiteConnectionStringBuilder CreateSQLiteConnectionStringBuilder()
    {
        return new()
        {
            DataSource = "Data Source=stars.sqlite"
        };
    }

    #endregion

}
