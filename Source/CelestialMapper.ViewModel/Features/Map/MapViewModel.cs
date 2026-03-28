using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using System.Globalization;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(MapViewModel), IsSingleton = false, Key = nameof(MapViewModel))]
[PaperItemIdentifier(Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "DefaultMap")]
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
        DateTime = now.Date;
        Time = now.TimeOfDay;
        var (lon, lat) = this.timeLocationHelper.Location;
        LongitudeInput = lon.ToString(CultureInfo.InvariantCulture);
        LatitudeInput = lat.ToString(CultureInfo.InvariantCulture);

        ApplyCommand = new RelayCommand(o =>
        {
            GenerateMap(null);
            RefreshInputs();
        });
        GenerateMapCommand = new RelayCommand(o => GenerateMap(o));
    }

    #endregion

    #region Commands

    public ICommand? GenerateMapCommand { get; private set; }

    #endregion

    #region Properties

    public IReadOnlySet<CelestialObject> CelestialObjects => this.map?.CelestialObjects ?? new HashSet<CelestialObject>();

    public IReadOnlySet<Constellation> Constellations => this.map?.Constellations ?? new HashSet<Constellation>();

    public override PaperItemType ItemType => PaperItemType.Map;

    public ICommand? ApplyCommand { get; private set; }

    public DateTime DateTime
    {
        get => GetPropertyValue<DateTime>();
        set => SetPropertyValue(value);
    }

    public TimeSpan Time
    {
        get => GetPropertyValue<TimeSpan>();
        set => SetPropertyValue(value);
    }

    public double Latitude
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double Longitude
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public string LatitudeInput
    {
        get => GetPropertyValue<string>() ?? Latitude.ToString();
        set
        {
            if (!SetPropertyValue(value))
            {
                return;
            }

            Latitude = ParseDouble(value);
        }
    }

    public string LongitudeInput
    {
        get => GetPropertyValue<string>() ?? Longitude.ToString();
        set
        {
            if (!SetPropertyValue(value))
            {
                return;
            }

            Longitude = ParseDouble(value);
        }
    }
    #endregion

    #region Methods

    private void GenerateMap(object? o)
    {
        var task = this.mapManager.Generate(
                new(Latitude, Longitude),
                DateTime.WithTimeOfDay(Time),
                IGenerateMapSettings.Create(NumRange.Of(-1d, 5d)));
        task.Wait();
        this.map = task.Result;

        RisePropertyChanged(nameof(CelestialObjects), nameof(Constellations));
    }

    #endregion

    #region Helpers

    private double ParseDouble(string value)
    {
        if (double.TryParse(value, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        return 0;
    }

    private void RefreshInputs()
    {
        if (LatitudeInput.IsNullOrEmpty())
        {
            LatitudeInput = "0";
        }

        if (LongitudeInput.IsNullOrEmpty())
        {
            LongitudeInput = "0";
        }
    }

    #endregion

}
