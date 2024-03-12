namespace CelestialMapper.Common;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

[Export(typeof(IIoCManager), typeof(IoCManager), IsSingleton = true, Key = nameof(IoCManager))]
public class IoCManager : IIoCManager
{

    private readonly IServiceCollection services;
    private ServiceProvider? serviceProvider;

    public IoCManager(IServiceCollection services)
    {
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IServiceProvider ServiceProvider => this.serviceProvider ??= this.services.BuildServiceProvider();

    public void RegisterExports(Assembly assembly)
    {
        if (assembly is null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        var exportAttributes = assembly
            .GetTypes()
            .SelectMany(type => type.GetCustomAttributes<ExportAttribute>(false));

        if (exportAttributes.IsNullOrEmpty())
        {
            return;
        }

        foreach (var exportAttribute in exportAttributes)
        {
            if (exportAttribute is not null)
            {
                Register(exportAttribute);
            }
        }
    }

    public void RegisterItself()
    {
        this.services.AddSingleton(this.services);
        this.services.AddSingleton<IIoCManager>(this);
    }

    private void Register(ExportAttribute exportAttribute)
    {
        if (exportAttribute.IsSingleton)
        {
            this.services.AddSingleton(exportAttribute.Interface, exportAttribute.Implementation);
        }
        else
        {
            this.services.AddTransient(exportAttribute.Interface, exportAttribute.Implementation);
        }
    }

}
