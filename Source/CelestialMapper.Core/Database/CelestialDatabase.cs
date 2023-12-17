using CelestialMapper.Common;
using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Database;

public class CelestialDatabase : ICelestialDatabase
{

    #region Constructors

    public CelestialDatabase()
    {
        
    }

    #endregion

    #region Methods

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CelestialObject> GetCelestialObjects(Geographic location, DateTime dateTime, NumRange<double> magnitudeRange)
    {
        throw new NotImplementedException();
    }

    #endregion

}
