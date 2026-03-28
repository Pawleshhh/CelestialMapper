using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

public interface ILocationProvider
{
    public Geographic GetLocation();
}
