namespace CelestialMapper.Common;

using System.Reflection;

public interface IIoCManager
{

    public IServiceProvider ServiceProvider { get; }

    public void RegisterExports(Assembly assembly);

    public void RegisterItself();

}
