using Microsoft.Extensions.DependencyInjection;

namespace CelestialMapper.UI;

public class PlatformItemsControl : ItemsControl
{

    protected IResourceResolver resourceResolver;

    public PlatformItemsControl()
    {
        ServiceProvider = App.ServiceProvider;
        this.resourceResolver = GetResourceResolver();
    }

    public PlatformItemsControl(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        this.resourceResolver = GetResourceResolver();
    }

    protected IServiceProvider ServiceProvider { get; }

    protected object GetResource(string key)
        => this.resourceResolver.ResolveResource(key);

    private IResourceResolver GetResourceResolver()
    {
        return ServiceProvider.GetService<IResourceResolver>()
            ?? throw new InvalidOperationException("Could not find IResourceResolver service");
    }

}
