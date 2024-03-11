namespace CelestialMapper.ViewModel;

public interface IViewModelSupport
{
    public IResourceResolver ResourceResolver { get; }
}

[Export(typeof(IViewModelSupport), typeof(ViewModelSupport), IsSingleton = true, Key = nameof(ViewModelSupport))]
public class ViewModelSupport : IViewModelSupport
{

    public ViewModelSupport(IResourceResolver resourceResolver)
    {
        ResourceResolver = resourceResolver;
    }

    public IResourceResolver ResourceResolver { get; }
}
