using CelestialMapper.Common;

namespace CelestialMapper.Core.Database;

[Export(typeof(ICelestialObjectProcessor), typeof(CelestialObjectProcessor), IsSingleton = true, Key = nameof(CelestialObjectProcessor))]
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
