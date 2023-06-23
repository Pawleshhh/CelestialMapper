using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Infrastructure.Map;

public class MapManager : IMapManager
{

    #region Fields

    private readonly ICelestialDatabase celestialDatabase;

    #endregion

    #region Constructors

    public MapManager(ICelestialDatabase celestialDatabase)
    {
        this.celestialDatabase = celestialDatabase;
    }

    #endregion

    #region Properties

    #endregion

    #region Methods

    public async Task<IMap> Generate(Geographic location, DateTime dateTime, GenerateMapSettings generateMapSettings)
    {
        var celestialObjects = await this.celestialDatabase.GetCelestialObjects(location, dateTime, generateMapSettings);

        return CreateMap(celestialObjects);
    }

    protected virtual IMap CreateMap(IEnumerable<CelestialObject> celestialObjects)
        => new CelestialMap(celestialObjects);

    #endregion

}
