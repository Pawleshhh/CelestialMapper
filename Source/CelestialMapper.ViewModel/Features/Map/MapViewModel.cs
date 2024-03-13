using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapViewModel), IsSingleton = true, Key = nameof(MapViewModel))]
public class MapViewModel : ViewModelBase
{

    #region Fields

    private readonly IMapManager mapManager;

    private IMap map = default!;

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

        GenerateMapCommand = new RelayCommand(GenerateMap);
    }

    #endregion

    #region Commands

    public ICommand? GenerateMapCommand { get; private set; }

    #endregion

    #region Properties

    public IReadOnlySet<CelestialObject> CelestialObjects => this.map?.CelestialObjects ?? new HashSet<CelestialObject>();

    #endregion

    #region Methods

    private async void GenerateMap(object? o)
    {
        this.map = await this.mapManager.Generate(
            new(0, 0), 
            DateTime.Now, 
            IGenerateMapSettings.Create(NumRange.Of(0d, 2d)));

        RisePropertyChanged(nameof(CelestialObjects));
    }

    #endregion

}
