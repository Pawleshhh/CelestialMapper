using System.Data.Common;

namespace CelestialMapper.Core.Database;

public interface IDatabaseWrapper
{

    public IEnumerable<T> Query<T>(DbConnection dbConnection, string query);

    public DbConnection CreateDbConnection(DbConnectionStringBuilder dbConnectionStringBuilder);
}
