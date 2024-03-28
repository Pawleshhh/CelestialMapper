using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using PracticalAstronomy.CSharp;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapViewModel), IsSingleton = true, Key = nameof(MapViewModel))]
public class MapViewModel : ViewModelBase
{

    #region Fields

    private readonly IMapManager mapManager;
    private readonly ITimeMachineManager timeMachineManager;

    private IMap map = default!;

    #endregion

    #region Constructors

    public MapViewModel(
        IMapManager mapManager,
        ITimeMachineManager timeMachineManager,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.mapManager = mapManager;
        this.timeMachineManager = timeMachineManager;
    }

    #endregion

    #region Base

    public override string DefaultFeatureName => FeatureNames.Map;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        GenerateMapCommand = new RelayCommand(o => GenerateMap(o));
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        this.timeMachineManager.LocationChanged += TimeMachineManager_LocationChanged;
        this.timeMachineManager.DateTimeChanged += TimeMachineManager_DateTimeChanged;
        this.timeMachineManager.TimeMachineUpdated += TimeMachineManager_TimeMachineUpdated;
    }

    protected override void UnsubscribeFromEvents()
    {
        base.UnsubscribeFromEvents();
        this.timeMachineManager.LocationChanged -= TimeMachineManager_LocationChanged;
        this.timeMachineManager.DateTimeChanged -= TimeMachineManager_DateTimeChanged;
        this.timeMachineManager.TimeMachineUpdated -= TimeMachineManager_TimeMachineUpdated;
    }


    #endregion

    #region Event handlers

    private void TimeMachineManager_TimeMachineUpdated(PlatformEventArgs<ITimeMachineManager, (DateTime DateTime, Geographic Location)> e)
    {
        GenerateMapCommand?.Execute(null);
    }

    private void TimeMachineManager_DateTimeChanged(PlatformEventArgs<ITimeMachineManager, DateTime> e)
    {
        GenerateMapCommand?.Execute(null);
    }

    private void TimeMachineManager_LocationChanged(PlatformEventArgs<ITimeMachineManager, Geographic> e)
    {
        GenerateMapCommand?.Execute(null);
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

    private void GenerateMap(object? o)
    {
        var dateTime = this.timeMachineManager.DateTime;
        dateTime = dateTime.ToUniversalTime();

        this.map = this.mapManager.Generate(
                this.timeMachineManager.Location,
                dateTime,
                IGenerateMapSettings.Create(NumRange.Of(-1d, 5d))).Result;

        RisePropertyChanged(nameof(CelestialObjects), nameof(Constellations));

        //while (true)
        //{
        //    this.map = this.mapManager.Generate(
        //        new(53.482906986790525, 14.862220332070006),
        //        dateTime,
        //        IGenerateMapSettings.Create(NumRange.Of(-1d, 5d))).Result;

        //    RisePropertyChanged(nameof(CelestialObjects), nameof(Constellations));

        //    await Task.Delay(200);

        //    dateTime = dateTime.AddHours(0.1);
        //}
    }

    #endregion

}
