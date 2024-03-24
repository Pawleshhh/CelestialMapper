using CelestialMapper.Common;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;
using System.Data.SQLite;
using System.Globalization;

namespace CelestialMapper.Core;

internal abstract class SQLiteHelperBase
{
    #region Fields

    protected readonly Func<int> getProcessorCount;

    protected readonly IDatabaseWrapper dbWrapper;
    protected readonly ICelestialObjectProcessor celestialObjectProcessor;
    protected readonly SQLiteConnectionStringBuilder connectionBuilder;

    protected readonly int parallelThreshold = 10_000;

    #endregion

    public SQLiteHelperBase(
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

    #region Helpers

    protected static string MagnitudeCondition(NumRange<double> magnitude)
    {
        return $"{DbColumnNames.StarsColumnNames.Magnitude} BETWEEN {magnitude.Min} AND {magnitude.Max}";
    }

    protected static string AboveHorizonCondition(string starTableName, Geographic location, double horizon = 0)
    {
        starTableName = NormalizeTableName(starTableName);
        return $"(90 - " +
            $"{FormatDouble(location.Latitude)} + " +
            $"{starTableName}{DbColumnNames.StarsColumnNames.Declination}) >= " +
            $"{FormatDouble(horizon)}";
    }

    protected static string SkyContainsCondition(Geographic location, DateTime dateTime)
    {
        return $"SKYCONTAINS({DbColumnNames.StarsColumnNames.RightAcension}, {DbColumnNames.StarsColumnNames.Declination}, " +
            $"'{dateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)}', " +
            $"{FormatDouble(location.Latitude)}, " +
            $"{FormatDouble(location.Longitude)})";
    }

    protected static string FormatDouble(double value)
    {
        return value.ToString("N6", CultureInfo.InvariantCulture);
    }

    protected static string NormalizeTableName(string tableName)
    {
        if (!string.IsNullOrEmpty(tableName))
        {
            tableName += ".";
        }
        return tableName;
    }

    #endregion

}
