using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using System.Globalization;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapViewModel), IsSingleton = false, Key = nameof(MapViewModel))]
[PaperItemIdentifier(Category = PaperItemCategory.Map, ItemType = PaperItemType.Map, NameKey = "DefaultMap")]
public class MapViewModel : PaperItemBaseViewModel
{

    #region Fields

    private readonly IMapManager mapManager;
    private readonly TimeLocationHelper timeLocationHelper;

    private IMap map = default!;

    #endregion

    #region Constructors

    public MapViewModel(
        IMapManager mapManager,
        IViewModelSupport viewModelSupport,
        TimeLocationHelper timeLocationHelper) : base(viewModelSupport)
    {
        this.mapManager = mapManager;
        this.timeLocationHelper = timeLocationHelper;

        Id = Guid.NewGuid();
    }

    #endregion

    #region Base

    public override FeatureName DefaultFeatureName => FeatureNames.Map;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        var now = this.timeLocationHelper.DateTime;
        DateTime.Value = now.Date;
        Time.Value = now.TimeOfDay;
        var (lon, lat) = this.timeLocationHelper.Location;

        ApplyCommand.Value = new RelayCommand(o =>
        {
            GenerateMap(null);
        });
        GenerateMapCommand = new RelayCommand(o => GenerateMap(o));
    }

    #endregion

    #region Commands

    public ICommand? GenerateMapCommand { get; private set; }

    #endregion

    #region Properties

    public IReadOnlySet<CelestialObjectVisualData> CelestialObjects => GetCelestialObjectVisualData();

    public IReadOnlySet<Constellation> Constellations => this.map?.Constellations ?? new HashSet<Constellation>();

    public override PaperItemType ItemType => PaperItemType.Map;

    public PropertyWrapper<ICommand?> ApplyCommand { get; private set; } = new(nameof(ApplyCommand));

    public PropertyWrapper<DateTime> DateTime { get; } = new(nameof(DateTime));

    public PropertyWrapper<TimeSpan> Time { get; } = new(nameof(Time));

    public PropertyWrapper<double> Latitude { get; } = new(nameof(Latitude));

    public PropertyWrapper<double> Longitude { get; } = new(nameof(Longitude));

    public PropertyWrapper<double> Magnitude { get; } = new(6, nameof(Magnitude));

    #endregion

    #region Methods

    public override void InitializeProperties()
    {
        base.InitializeProperties();
        this.Properties.AddRange(new IPropertyWrapper[] { DateTime, Time, Latitude, Longitude, Magnitude, ApplyCommand });
    }

    private void GenerateMap(object? o)
    {
        var task = this.mapManager.Generate(
                new(Latitude.Value, Longitude.Value),
                DateTime.Value.WithTimeOfDay(Time.Value),
                IGenerateMapSettings.Create(NumRange.Of(-1d, Magnitude.Value)));
        task.Wait();
        this.map = task.Result;

        RisePropertyChanged(nameof(CelestialObjects), nameof(Constellations));
    }

    #endregion

    #region Helpers

    private HashSet<CelestialObjectVisualData> GetCelestialObjectVisualData()
    {
        if (this.map is null)
        {
            return new HashSet<CelestialObjectVisualData>();
        }

        return this.map.CelestialObjects.Select(GetCelestialObjectVisualData).ToHashSet();
    }

    private CelestialObjectVisualData GetCelestialObjectVisualData(CelestialObject celestialObject)
    {
        return new VisualStarData(celestialObject);
    }

    private double ParseDouble(string value)
    {
        if (double.TryParse(value, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        return 0;
    }

    #endregion

}
