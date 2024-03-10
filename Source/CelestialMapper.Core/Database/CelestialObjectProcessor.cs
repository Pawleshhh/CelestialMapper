using CSharpUtilities;

namespace CelestialMapper.Core.Database;

internal class CelestialObjectProcessor : ICelestialObjectProcessor
{
    public void Process<T>(IEnumerable<T> data, Action<T> process, bool parallel = false)
    {
        if (parallel)
        {
            Parallel.ForEach(data, process);
            return;
        }

        data.ForEach(process);
    }
}
