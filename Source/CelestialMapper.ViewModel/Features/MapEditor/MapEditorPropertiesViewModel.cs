namespace CelestialMapper.ViewModel;

[Export(typeof(MapEditorPropertiesViewModel), IsSingleton = false, Key = nameof(MapEditorPropertiesViewModel))]
public class MapEditorPropertiesViewModel : ViewModelBase, IMenuItemViewModel
{
    public MapEditorPropertiesViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    public bool IsAvailable
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditorProperties;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }

    public MapViewModel MapVM
    {
        get => GetPropertyValue<MapViewModel>()!;
        set => SetPropertyValue(value);
    }
}
