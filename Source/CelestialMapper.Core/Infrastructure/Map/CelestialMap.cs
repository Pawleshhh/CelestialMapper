using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;
using System.Collections.Immutable;

namespace CelestialMapper.Core.Infrastructure.Map;

public class CelestialMap : IMap
{

    public CelestialMap(IEnumerable<CelestialObject> celestialObjects)
    {
        CelestialObjects = celestialObjects.ToImmutableHashSet();
    }

    public IGenerateMapSettings GenerateMapSettings { get; init; } 
        = IGenerateMapSettings.Create(MapConstants.DefaultMagnitudeRange);

    public IReadOnlySet<CelestialObject> CelestialObjects { get; }

    public Geographic Location { get; init; } = MapConstants.DefaultLocation;
    public DateTime DateTime { get; init; } = DateTime.UtcNow;

}
