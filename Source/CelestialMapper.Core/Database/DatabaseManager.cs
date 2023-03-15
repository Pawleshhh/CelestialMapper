using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace CelestialMapper.Core;

public class DatabaseManager
{

    private static readonly string databasePath = "./stars.sqlite";

    public IEnumerable<CelestialObject> GetCelestialObjects(Func<CelestialObject, bool>? predicate = null)
    {
        predicate ??= _ => true;

        using DbConnection connection = new SqliteConnection($"Data Source={databasePath}");
        connection.Open();

        using DbCommand command = connection.CreateCommand();

        throw new NotImplementedException();
    }

}
