using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Infrastructure.Map;

public interface IMap
{

    public Guid Id { get; }

    public IGenerateMapSettings GenerateMapSettings { get; }

    public IReadOnlySet<CelestialObject> CelestialObjects { get; }

    public IReadOnlySet<Constellation> Constellations { get; }

    public Geographic Location { get; init; }

    public DateTime DateTime { get; init; }

}
