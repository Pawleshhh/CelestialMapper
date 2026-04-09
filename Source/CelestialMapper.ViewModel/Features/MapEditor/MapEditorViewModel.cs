using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using System.Globalization;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapEditorViewModel), IsSingleton = false, Key = nameof(MapEditorViewModel))]
public class MapEditorViewModel : ViewModelBase
{

    #region Fields

    private readonly IMapManager mapManager;
    private readonly TimeLocationHelper timeLocationHelper;

    #endregion

    #region Constructors

    public MapEditorViewModel(
        IMapManager mapManager,
        TimeLocationHelper timeLocationHelper,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    #endregion

    #region Base

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditor;

    public override void Initialize(IViewModelConfigurator configurator, object? data)
    {
        base.Initialize(configurator, data);

        MapVM = (MapViewModel)data!;
        MapEditorMenuVM = GetViewModel<MapEditorPropertiesViewModel>(FeatureNames.MapEditorMenu, vm => vm.MapVM = MapVM);
    }

    #endregion

    #region Commands

    #endregion

    #region Properties

    public MapViewModel MapVM
    {
        get => GetPropertyValue<MapViewModel>()!;
        set => SetPropertyValue(value);
    }

    public MapEditorPropertiesViewModel MapEditorMenuVM
    {
        get => GetPropertyValue<MapEditorPropertiesViewModel>()!;
        set => SetPropertyValue(value);
    }

    #endregion

    #region Methods

    #endregion

    #region Helpers

    #endregion

}
