using Microsoft.Extensions.DependencyInjection;

namespace CelestialMapper.Common;

public static class Bootstrapper
{

    private static readonly ServiceCollection serviceCollection =
        new ServiceCollection();

    private static readonly IIoCManager iocManager =
        new IoCManager(serviceCollection);

    private static readonly List<System.Reflection.Assembly> assemblies = new()
    {
        typeof(CelestialMapper.Common.CommonAssembly).Assembly,
        typeof(CelestialMapper.Core.CoreAssembly).Assembly,
        typeof(CelestialMapper.ViewModel.ViewModelAssembly).Assembly,
    };

    public static IServiceProvider Boot(System.Reflection.Assembly booter)
    {
        foreach (var assembly in assemblies.Append(booter))
        {
            iocManager.RegisterExports(assembly);
        }

        iocManager.RegisterItself();

        return iocManager.ServiceProvider;
    }

}
