using CelestialMapper.Core.Astronomy;

namespace CelestialMapper.Core.Database;

public interface ICelestialObjectProcessor
{

    void Process<T>(IEnumerable<T> data, Action<T> process, bool parallel = false);

}
