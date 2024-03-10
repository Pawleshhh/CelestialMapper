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

    public Task<IMap> Generate(Geographic location, DateTime dateTime, IGenerateMapSettings generateMapSettings)
    {
        return Task.Run(() =>
        {
            var celestialObjects = this.celestialDatabase.GetCelestialObjects(
                location, 
                dateTime, 
                generateMapSettings.MagnitudeRange);
            return CreateMap(location, dateTime, generateMapSettings, celestialObjects);
        });
    }

    protected virtual IMap CreateMap(
        Geographic location,
        DateTime dateTime,
        IGenerateMapSettings generateMapSettings,
        IEnumerable<CelestialObject> celestialObjects)
    {
        return new CelestialMap(celestialObjects)
        {
            Location = location,
            DateTime = dateTime
        };
    }

    #endregion

}
