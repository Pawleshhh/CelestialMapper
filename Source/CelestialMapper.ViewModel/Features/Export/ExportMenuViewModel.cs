namespace CelestialMapper.ViewModel;

[Export(typeof(ExportMenuViewModel), IsSingleton = true, Key = nameof(ExportMenuViewModel))]
public class ExportMenuViewModel : ViewModelBase, IMenuItemViewModel
{
    public ExportMenuViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    public override FeatureName DefaultFeatureName => FeatureNames.ExportMenu;

    public bool IsAvailable
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
        IsAvailable = true;
    }
}
