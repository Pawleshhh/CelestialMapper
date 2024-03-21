using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;
using System.Data.SQLite;

namespace CelestialMapper.Core;

internal class SQLiteConstellationsHelper : SQLiteHelperBase
{

    public SQLiteConstellationsHelper(
        IDatabaseWrapper dbWrapper,
        ICelestialObjectProcessor celestialObjectProcessor,
        SQLiteConnectionStringBuilder connectionBuilder,
        int parallelThreshold,
        Func<int> getProcessorCount)
        : base(dbWrapper, celestialObjectProcessor, connectionBuilder, parallelThreshold, getProcessorCount)
    {

    }

    public IEnumerable<Constellation> GetConstellations(Geographic location, DateTime dateTime)
    {
        using var connection = this.dbWrapper.CreateDbConnection(this.connectionBuilder);
        connection.Open();

        string constellationTableName = "cl";
        string starTableName = "st";

        string query =
            $"SELECT {GetAllConstellationLineColumns(starTableName, constellationTableName)} " +
            $"FROM {DbColumnNames.StarsColumnNames.TableName} AS {starTableName}, {DbColumnNames.ConstellationLinesColumnNames.TableName} AS {constellationTableName} " +
            $"WHERE {AboveHorizonCondition(location)} " +
            $"AND {SkyContainsCondition(location, dateTime)} " +
            $"AND {ConstellationLineCondition(starTableName, constellationTableName)} " +
            $"ORDER BY cl.id";

        var rows = this.dbWrapper.Query<ConstellationLineDataRowPosition>(connection, query);

        var constellations = CreateConstellations(location, dateTime, rows).ToArray();
        connection.Close();

        return constellations;
    }

    private static IEnumerable<Constellation> CreateConstellations(Geographic location, DateTime dateTime, IEnumerable<ConstellationLineDataRowPosition> lineRows)
    {
        var groupedLines = lineRows.GroupBy(x => x.Con);

        long id = 1;
        foreach (var group in groupedLines)
        {
            List<ConstellationLine> lines = new();
            var lineCoords = GetHorizonCoords(group);
            for (int i = 0; i < lineCoords.Length - 1; i++)
            {
                if (lineCoords[i].LineId != lineCoords[i + 1].LineId)
                {
                    continue;
                }

                lines.Add(new(lineCoords[i].Horizon, lineCoords[i + 1].Horizon));
            }

            yield return new(id++, group.Key, group.Key, lines);
        }

        (Horizon Horizon, int LineId)[] GetHorizonCoords(IEnumerable<ConstellationLineDataRowPosition> rows)
            => rows.Select(x =>
            {
                var hourAngle = PA.CoordinateSystems.RightAscensionToHourAngle(dateTime, location.Longitude, x.Ra * 15d);
                var horizon = PA.CoordinateSystems.EquatorialToHorizon(location.Latitude, new(hourAngle, x.Dec));
                return (horizon, x.LineId);
            }).ToArray();
    }

    private static string GetAllConstellationLineColumns(string starTableName, string constellationTableName)
    {
        starTableName = NormalizeTableName(starTableName);
        constellationTableName = NormalizeTableName(constellationTableName);

        return $"{CreateName(constellationTableName, DbColumnNames.ConstellationLinesColumnNames.Id)}, " +
            $"{CreateName(constellationTableName, DbColumnNames.ConstellationLinesColumnNames.ConstellationName)}, " +
            $"{CreateName(constellationTableName, DbColumnNames.ConstellationLinesColumnNames.LineId)}, " +
            $"{CreateName(constellationTableName, DbColumnNames.ConstellationLinesColumnNames.HarvardYaleBrightStarCatalogId)}, " +
            $"{CreateName(starTableName, DbColumnNames.StarsColumnNames.RightAcension)}, " +
            $"{CreateName(starTableName, DbColumnNames.StarsColumnNames.Declination)}";

        string CreateName(string tableName, string columnName)
            => tableName + columnName;
    }

    private static string ConstellationLineCondition(string starTableName, string constellationTableName)
    {
        starTableName = NormalizeTableName(starTableName);
        constellationTableName = NormalizeTableName(constellationTableName);

        return $"{constellationTableName}{DbColumnNames.ConstellationLinesColumnNames.HarvardYaleBrightStarCatalogId} == " +
            $"{starTableName}{DbColumnNames.StarsColumnNames.HarvardYaleBrightStarCatalogId}";
    }

    private static string NormalizeTableName(string tableName)
    {
        if (!string.IsNullOrEmpty(tableName))
        {
            tableName += ".";
        }
        return tableName;
    }
}
