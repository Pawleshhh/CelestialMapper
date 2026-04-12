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
    private readonly MapEditorHelper mapEditorHelper;

    #endregion

    #region Constructors

    public MapEditorViewModel(
        IMapManager mapManager,
        TimeLocationHelper timeLocationHelper,
        MapEditorHelper mapEditorHelper,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.mapEditorHelper = mapEditorHelper;
    }

    #endregion

    #region Base

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditor;

    public override void Initialize(IViewModelConfigurator configurator, object? data)
    {
        base.Initialize(configurator, data);

        MapVM = this.mapEditorHelper.MapToEdit ?? throw new InvalidOperationException("Expected active map to edit");
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

    public MapEditorPropertiesMenuViewModel MapEditorMenuVM
    {
        get => GetPropertyValue<MapEditorPropertiesMenuViewModel>()!;
        set => SetPropertyValue(value);
    }

    #endregion

    #region Methods

    #endregion

    #region Helpers

    #endregion

}
