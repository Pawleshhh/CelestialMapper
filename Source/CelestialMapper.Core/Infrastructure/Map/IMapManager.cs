using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Infrastructure.Map;

public interface IMapManager
{
    public Task<IMap> Generate(Geographic location, DateTime dateTime, IGenerateMapSettings generateMapSettings);
}
