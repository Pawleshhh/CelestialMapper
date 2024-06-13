namespace CelestialMapper.ViewModel;

[Export(typeof(ExportMenuViewModel), IsSingleton = true, Key = nameof(ExportMenuViewModel))]
public class ExportMenuViewModel : ViewModelBase
{
    public ExportMenuViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    public override string DefaultFeatureName => FeatureNames.ExportMenu;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }
}
