using CelestialMapper.Core.Infrastructure.Map;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapViewModel), IsSingleton = true, Key = nameof(MapViewModel))]
public class MapViewModel : ViewModelBase
{

    #region Fields

    private readonly IMapManager mapManager;

    #endregion

    #region Constructors

    public MapViewModel(
        IMapManager mapManager,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.mapManager = mapManager;
    }

    #endregion

    #region Base

    public override string DefaultFeatureName => FeatureNames.Map;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }

    #endregion

}
