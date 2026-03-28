using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core;

[Export(typeof(ILocationProvider), typeof(LocationProvider), IsKeyed = false, IsSingleton = true, Key = nameof(LocationProvider))]
public class LocationProvider : ILocationProvider
{
    public Geographic GetLocation()
    {
        return new(0, 0);
    }
}
