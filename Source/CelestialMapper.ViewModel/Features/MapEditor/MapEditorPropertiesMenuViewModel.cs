namespace CelestialMapper.ViewModel;

[Export(typeof(MapEditorPropertiesMenuViewModel), IsSingleton = false, Key = nameof(MapEditorPropertiesMenuViewModel))]
public class MapEditorPropertiesMenuViewModel : ViewModelBase, IMenuItemViewModel
{
    private readonly MapEditorHelper mapEditorHelper;

    public MapEditorPropertiesMenuViewModel(
        MapEditorHelper mapEditorHelper,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.mapEditorHelper = mapEditorHelper;
    }

    public bool IsAvailable
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditorPropertiesMenu;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
        IsAvailable = true;

        MapVM = this.mapEditorHelper.MapToEdit ?? throw new InvalidOperationException("Expected active map to edit");
    }

    public MapViewModel MapVM
    {
        get => GetPropertyValue<MapViewModel>()!;
        set => SetPropertyValue(value);
    }
}
