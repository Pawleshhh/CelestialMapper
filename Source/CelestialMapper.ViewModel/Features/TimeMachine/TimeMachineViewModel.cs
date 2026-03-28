using PracticalAstronomy.CSharp;
using System.Globalization;
using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(TimeMachineViewModel), IsSingleton = true, Key = nameof(TimeMachineViewModel))]
public class TimeMachineViewModel : ViewModelBase
{

    #region Fields

    private readonly Geographic defaultLocation = new(0, 0);

    #endregion

    #region Constructors

    public TimeMachineViewModel(
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    #endregion

    #region ViewModelBase

    public override FeatureName DefaultFeatureName => FeatureNames.TimeMachine;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        var now = DateTime.Now;
        DateTime = now.Date;
        Time = now.TimeOfDay;

        ApplyCommand = new RelayCommand(o =>
        {
            RefreshInputs();
        });
    }

    #endregion

    #region Properties

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
