using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;
using System.Data.Common;

namespace CelestialMapper.Core.Database;

public interface ICelestialDatabase
{

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime);

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime, NumRange<double> magnitudeRange);

}
