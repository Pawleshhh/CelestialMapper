﻿using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;
using System.Collections.Immutable;

namespace CelestialMapper.Core.Infrastructure.Map;

[Export(typeof(IMapManager), typeof(MapManager), IsSingleton = true, Key = nameof(MapManager))]
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

    #endregion

}
