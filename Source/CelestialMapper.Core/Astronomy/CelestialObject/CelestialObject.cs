namespace CelestialMapper.Core;

public record CelestialObject(
    long Id,
    string Name,
    EquatorialCoordinates EquatorialCoordinates,
    HorizonCoordinates HorizonCoordinates, 
    double Magnitude);