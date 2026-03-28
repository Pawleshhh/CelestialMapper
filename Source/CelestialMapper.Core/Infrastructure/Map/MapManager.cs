using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;
using System;
using System.Collections.Immutable;

namespace CelestialMapper.Core.Infrastructure.Map;

[Export(typeof(IMapManager), typeof(MapManager), IsSingleton = true, Key = nameof(MapManager))]
public class MapManager : IMapManager
{

    #region Fields

    private readonly ICelestialDatabase celestialDatabase;
    private readonly ITimeMachineManager timeMachineManager;
    private readonly TimeLocationHelper timeLocationHelper;

    #endregion

    #region Constructors

    public MapManager(ICelestialDatabase celestialDatabase, ITimeMachineManager timeMachineManager, TimeLocationHelper timeLocationHelper)
    {
        this.celestialDatabase = celestialDatabase;
        this.timeMachineManager = timeMachineManager;
        this.timeLocationHelper = timeLocationHelper;
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
            var constellations = this.celestialDatabase.GetConstellations(
                location,
                dateTime);
            return CreateMap(location, dateTime, generateMapSettings, celestialObjects, constellations);
        });
    }

    public Task<IMap> Generate(IGenerateMapSettings generateMapSettings)
    {
        return Task.Run(() =>
        {
            var guid = Guid.NewGuid();
            var (dateTime, location) = (this.timeLocationHelper.DateTime, this.timeLocationHelper.Location);
            this.timeMachineManager.Update(guid, dateTime, location);

            var celestialObjects = this.celestialDatabase.GetCelestialObjects(
                location,
                dateTime,
                generateMapSettings.MagnitudeRange);
            var constellations = this.celestialDatabase.GetConstellations(
                location,
                dateTime);
            return CreateMap(location, dateTime, generateMapSettings, celestialObjects, constellations);
        });
    }

    protected virtual IMap CreateMap(
        Geographic location,
        DateTime dateTime,
        IGenerateMapSettings generateMapSettings,
        IEnumerable<CelestialObject> celestialObjects,
        IEnumerable<Constellation> constellations)
    {
        return new CelestialMap(celestialObjects)
        {
            Location = location,
            DateTime = dateTime,
            Constellations = constellations.ToImmutableHashSet()
        };
    }

    protected virtual IMap CreateMap(
        Guid guid,
        Geographic location,
        DateTime dateTime,
        IGenerateMapSettings generateMapSettings,
        IEnumerable<CelestialObject> celestialObjects,
        IEnumerable<Constellation> constellations)
    {
        return new CelestialMap(guid, celestialObjects)
        {
            Location = location,
            DateTime = dateTime,
            Constellations = constellations.ToImmutableHashSet()
        };
    }

    #endregion

}
