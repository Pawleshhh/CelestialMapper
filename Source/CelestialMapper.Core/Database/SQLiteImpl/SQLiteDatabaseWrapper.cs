using Dapper;
using System.Data.Common;
using System.Data.SQLite;

namespace CelestialMapper.Core.Database.SQLiteImpl;

internal class SQLiteDatabaseWrapper : IDatabaseWrapper
{
    public IEnumerable<T> Query<T>(DbConnection dbConnection, string query)
    {
        return dbConnection.Query<T>(query);
    }

    public DbConnection CreateDbConnection(DbConnectionStringBuilder dbConnectionStringBuilder)
    {
        return new SQLiteConnection(dbConnectionStringBuilder.ConnectionString);
    }

}
