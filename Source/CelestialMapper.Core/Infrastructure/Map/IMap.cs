using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Infrastructure.Map;

public interface IMap
{

    public IGenerateMapSettings GenerateMapSettings { get; }

    public IReadOnlySet<CelestialObject> CelestialObjects { get; }

    public Geographic Location { get; init; }

    public DateTime DateTime { get; init; }

}
