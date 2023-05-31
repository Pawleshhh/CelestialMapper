using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

public record CelestialObject(
    long Id,
    string Name,
    EquatorialRightAscension EquatorialCoordinates,
    Horizon HorizonCoordinates, 
    double Magnitude);