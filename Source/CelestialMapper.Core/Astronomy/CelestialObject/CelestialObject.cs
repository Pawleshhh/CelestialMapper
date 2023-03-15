namespace CelestialMapper.Core;

public record CelestialObject(
    long Id,
    string Name, 
    HorizonCoordinates HorizonCoordinates, 
    double Magnitude);