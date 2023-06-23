namespace CelestialMapper.Core.Astronomy;

using PracticalAstronomy.CSharp;

public record CelestialObject(
    long Id,
    string Name, 
    Horizon HorizonCoordinates, 
    double Magnitude);