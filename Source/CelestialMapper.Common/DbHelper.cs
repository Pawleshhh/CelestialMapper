using System.Data.Common;

namespace CelestialMapper.Common;

public static class DbHelper
{

    public async static Task<T?> GetDataOrDefault<T>(this DbDataReader reader, int column, Func<int, T> getData)
        => await reader.IsDBNullAsync(column) ? default : getData(column);

}
