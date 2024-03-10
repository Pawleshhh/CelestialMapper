using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Infrastructure.Map;

internal static class MapConstants
{

    public static NumRange<double> DefaultMagnitudeRange = new(0, 4);

    public static Geographic DefaultLocation = new(0, 0);

}
