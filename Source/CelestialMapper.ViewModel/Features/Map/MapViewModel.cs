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

        GenerateMapCommand = new RelayCommand(o => _ = GenerateMap(o));
    }

    #endregion

    #region Commands

    public ICommand? GenerateMapCommand { get; private set; }

    #endregion

    #region Properties

    public IReadOnlySet<CelestialObject> CelestialObjects => this.map?.CelestialObjects ?? new HashSet<CelestialObject>();

    public IReadOnlySet<Constellation> Constellations => this.map?.Constellations ?? new HashSet<Constellation>();

    #endregion

    #region Methods

    private async Task GenerateMap(object? o)
    {
        var dateTime = new DateTime(2024, 3, 24, 9, 0, 0, DateTimeKind.Local);
        dateTime = dateTime.ToUniversalTime();

        while (true)
        {
            this.map = this.mapManager.Generate(
                new(53.482906986790525, 14.862220332070006),
                dateTime,
                IGenerateMapSettings.Create(NumRange.Of(-1d, 5d))).Result;

            RisePropertyChanged(nameof(CelestialObjects), nameof(Constellations));

            await Task.Delay(80);

            dateTime = dateTime.AddHours(0.1);
        }
    }

    #endregion

}
