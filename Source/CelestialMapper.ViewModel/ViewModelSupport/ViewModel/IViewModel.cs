using System.ComponentModel;

namespace CelestialMapper.ViewModel;

public interface IViewModel : INotifyPropertyChanged
{

    public bool IsInitialized { get; }

    public FeatureName FeatureName { get; }
    public string Name { get; }

    public void Initialize(IViewModelConfigurator configurator);

    public void Unitilialize();

    public IViewModelConfigurator GetViewModelConfigurator(FeatureName featureName)
    {
        return InitializeConfigurators()[featureName];
    }

    protected Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators();

}
