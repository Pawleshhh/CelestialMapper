using Microsoft.Extensions.DependencyInjection;

namespace CelestialMapper.UI;

public class PlatformUserControl : UserControl
{
    protected IResourceResolver resourceResolver;

    public PlatformUserControl()
    {
        ServiceProvider = App.ServiceProvider;
        this.resourceResolver = GetResourceResolver();
    }

    public PlatformUserControl(IServiceProvider serviceProvider)
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
