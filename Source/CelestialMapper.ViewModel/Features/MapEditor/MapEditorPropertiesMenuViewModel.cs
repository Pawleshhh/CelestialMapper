using System.Collections.ObjectModel;

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

        Refresh();
    }

    protected override void SubscribeToEvents()
    {
        PropertyChanged += MapEditorPropertiesMenuViewModel_PropertyChanged;
    }

    protected override void UnsubscribeFromEvents()
    {
        PropertyChanged -= MapEditorPropertiesMenuViewModel_PropertyChanged;
        MapVM.PropertyChanged -= MapVM_PropertyChanged;
    }

    public ObservableCollection<IPropertyWrapper> MapProperties { get; private set; }

    public MapViewModel MapVM
    {
        get => GetPropertyValue<MapViewModel>()!;
        set => SetPropertyValue(value);
    }

    private void MapVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Refresh();
    }

    private void MapEditorPropertiesMenuViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MapVM))
        {
            MapVM.PropertyChanged += MapVM_PropertyChanged;
        }
    }

    private void Refresh()
    {
        if (MapVM is null)
        {
            return;
        }

        MapProperties = new(MapVM.Properties.Where(p => !ReferenceEquals(p, MapVM.EditMapCommand)));
    }
}
