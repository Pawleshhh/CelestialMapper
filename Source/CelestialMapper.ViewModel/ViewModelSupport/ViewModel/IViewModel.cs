using System.ComponentModel;

namespace CelestialMapper.ViewModel;

public interface IViewModel : INotifyPropertyChanged
{

    public string FeatureName { get; }
    public string Name { get; }

    public void Initialize(IViewModelConfigurator configurator);

    public void Unitilialize();

    public IViewModelConfigurator GetViewModelConfigurator(string featureName)
    {
        return InitializeConfigurators()[featureName];
    }

    protected Dictionary<string, IViewModelConfigurator> InitializeConfigurators();

}
