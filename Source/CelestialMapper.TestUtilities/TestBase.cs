using Moq;
using System.Reflection;

namespace CelestialMapper.TestUtilities;

public abstract class TestBase<T>
    where T : class
{

    public virtual Func<T> CreateSUT => () => default!;

    #region Test event

    #endregion

}
