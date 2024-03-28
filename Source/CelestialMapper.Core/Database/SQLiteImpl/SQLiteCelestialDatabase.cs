using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database.CustomFunctions;
using CelestialMapper.Core.Infrastructure.Map;
using PracticalAstronomy.CSharp;
using System.Data.SQLite;

namespace CelestialMapper.Core.Database.SQLiteImpl;

[Export(typeof(ICelestialDatabase), typeof(SQLiteCelestialDatabase), IsSingleton = true, Key = nameof(SQLiteCelestialDatabase))]
public class SQLiteCelestialDatabase : ICelestialDatabase
{

    #region Fields

    private readonly Func<int> getProcessorCount;

    private readonly IDatabaseWrapper dbWrapper;
    private readonly ICelestialObjectProcessor celestialObjectProcessor;
    private readonly SQLiteConnectionStringBuilder connectionBuilder;

    private const int ParallelThreshold = 10_000;

    private readonly SQLiteCelestialObjectHelper celestialObjectHelper;
    private readonly SQLiteConstellationsHelper constellationsHelper;

    #endregion

    #region Constructors

    static SQLiteCelestialDatabase()
    {
        SQLiteFunction.RegisterFunction(typeof(SkyContainsFunction));
    }

    public SQLiteCelestialDatabase(
        IDatabaseWrapper dbWrapper,
        ICelestialObjectProcessor celestialObjectProcessor,
        SQLiteConnectionStringBuilder? connectionStringBuilder = null,
        Func<int>? getProcessorCount = null)
    {
        this.dbWrapper = dbWrapper ?? throw new ArgumentNullException(nameof(dbWrapper));
        this.celestialObjectProcessor = celestialObjectProcessor ?? throw new ArgumentNullException(nameof(celestialObjectProcessor));
        this.connectionBuilder = connectionStringBuilder ?? CreateSQLiteConnectionStringBuilder();
        this.getProcessorCount = getProcessorCount ?? (() => Environment.ProcessorCount);

        this.celestialObjectHelper = new(
            this.dbWrapper,
            this.celestialObjectProcessor,
            this.connectionBuilder,
            ParallelThreshold,
            this.getProcessorCount);

        this.constellationsHelper = new(
            this.dbWrapper,
            this.celestialObjectProcessor,
            this.connectionBuilder,
            ParallelThreshold,
            this.getProcessorCount);
    }

    #endregion

    #region Methods

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime)
    {
        return this.celestialObjectHelper.GetCelestialObjects(location, dateTime, MapConstants.DefaultMagnitudeRange);
    }

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime, NumRange<double> magnitudeRange)
    {
        return this.celestialObjectHelper.GetCelestialObjects(location, dateTime, magnitudeRange);
    }
    
    public IEnumerable<Constellation> GetConstellations(Geographic location, DateTime dateTime)
    {
        return this.constellationsHelper.GetConstellations(location, dateTime);
    }

    #endregion

    #region Private methods

    private static SQLiteConnectionStringBuilder CreateSQLiteConnectionStringBuilder()
    {
        return new()
        {
            DataSource = "stars.sqlite"
        };
    }

    #endregion

}
