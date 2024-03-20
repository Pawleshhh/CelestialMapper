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
            $"SELECT {GetAllConstellationLineColumns(constellationTableName)} " +
            $"FROM {DbColumnNames.StarsColumnNames.TableName} AS {starTableName}, {DbColumnNames.ConstellationLinesColumnNames.TableName} AS {constellationTableName} " +
            $"WHERE {AboveHorizonCondition(location)} " +
            $"AND {SkyContainsCondition(location, dateTime)} " +
            $"AND {ConstellationLineCondition(starTableName, constellationTableName)}";

        var rows = this.dbWrapper.Query<ConstellationLineDataRow>(connection, query);

        var constellations = CreateConstellations(rows).ToArray();
        connection.Close();

        return constellations;
    }

    private static IEnumerable<Constellation> CreateConstellations(IEnumerable<ConstellationLineDataRow> lineRows)
    {
        var groupedLines = lineRows.GroupBy(x => x.Con);

        long id = 1;
        foreach (var group in groupedLines)
        {
            List<ConstellationLine> lines = new();
            var lineIds = GetLines(group);
            for (int i = 0; i < lineIds.Length - 1; i++)
            {
                if (lineIds[i].LineId != lineIds[i + 1].LineId)
                {
                    continue;
                }

                lines.Add(new(lineIds[i].Hr, lineIds[i + 1].Hr));
            }

            yield return new(id++, group.Key, group.Key, lines);
        }

        (int Hr, int LineId)[] GetLines(IEnumerable<ConstellationLineDataRow> rows)
            => rows.Select(x => (int.Parse(x.Hr), x.LineId)).ToArray();
    }

    private static string GetAllConstellationLineColumns(string tableName)
    {
        tableName = NormalizeTableName(tableName);

        return $"{CreateName(DbColumnNames.ConstellationLinesColumnNames.Id)}, " +
            $"{CreateName(DbColumnNames.ConstellationLinesColumnNames.ConstellationName)}, " +
            $"{CreateName(DbColumnNames.ConstellationLinesColumnNames.LineId)}, " +
            $"{CreateName(DbColumnNames.ConstellationLinesColumnNames.HarvardYaleBrightStarCatalogId)}";

        string CreateName(string columnName)
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
