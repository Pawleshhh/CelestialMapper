namespace CelestialMapper.ViewModel;

public interface IViewModelSupport
{
    public IResourceResolver ResourceResolver { get; }

    public IIoCManager IoCManager { get; }
}

[Export(typeof(IViewModelSupport), typeof(ViewModelSupport), IsSingleton = true, Key = nameof(ViewModelSupport))]
public class ViewModelSupport : IViewModelSupport
{

    public ViewModelSupport(IResourceResolver resourceResolver, IIoCManager ioCManager)
    {
        ResourceResolver = resourceResolver;
        IoCManager = ioCManager;
    }

    public IResourceResolver ResourceResolver { get; }

    public IIoCManager IoCManager { get; }
}
