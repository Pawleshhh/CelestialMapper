namespace CelestialMapper.UI;

using CelestialMapper.Common;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class TimePicker : PlatformUserControl
{

    public static readonly string TimePickerDefaultStyleKey = "Style.TimePicker";

    #region Constructors

    public TimePicker()
    {
        Style = TryFindResource("Style.TimePicker") as Style;
    }

    public TimePicker(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Style = TryFindResource("Style.TimePicker") as Style;
    }

    #endregion

    #region Properties

    private const string defaultTimePart = "00";

    public TimeSpan SelectedTime
    {
        get => this.GetValue<TimeSpan>(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    public static readonly DependencyProperty SelectedTimeProperty =
        Register(nameof(SelectedTime), new PlatformPropertyMetadata<TimePicker, TimeSpan>(TimeSpan.Zero, OnSelectedTimeChanged));

    private static void OnSelectedTimeChanged(TimePicker d, DependencyPropertyChangedEventArgs<TimeSpan> e)
    {
        var oldTimeSpan = e.OldValue;
        var newTimeSpan = e.NewValue;

        SetIfDiffer(t => t.Hours,   p => d.HourText = p);
        SetIfDiffer(t => t.Minutes, p => d.MinuteText = p);
        SetIfDiffer(t => t.Seconds, p => d.SecondText = p);

        void SetIfDiffer(Func<TimeSpan, int> getPart, Action<string> setPart)
        {
            var newPart = getPart(newTimeSpan);
            if (newPart != getPart(oldTimeSpan))
            {
                setPart(newPart.ToString(defaultTimePart));
            }
        }
    }

    public string HourText
    {
        get => this.GetValue<string>(HourTextProperty);
        set => SetValue(HourTextProperty, value);
    }

    public static readonly DependencyProperty HourTextProperty =
        Register(nameof(HourText), new PlatformPropertyMetadata<TimePicker, string>(defaultTimePart, OnHourTextChanged, CoerceTimeText));

    private static void OnHourTextChanged(TimePicker d, DependencyPropertyChangedEventArgs<string> e)
    {
        ConvertTextToTimeSpan(e.NewValue, d, t => t.Hours, TimeSpanExtension.WithHours);
    }

    public string MinuteText
    {
        get => this.GetValue<string>(MinuteTextProperty);
        set => SetValue(MinuteTextProperty, value);
    }

    public static readonly DependencyProperty MinuteTextProperty =
        Register(nameof(MinuteText), new PlatformPropertyMetadata<TimePicker, string>(defaultTimePart, OnMinuteTextChanged, CoerceTimeText));

    private static void OnMinuteTextChanged(TimePicker d, DependencyPropertyChangedEventArgs<string> e)
    {
        ConvertTextToTimeSpan(e.NewValue, d, t => t.Minutes, TimeSpanExtension.WithMinutes);
    }

    public string SecondText
    {
        get => this.GetValue<string>(SecondTextProperty);
        set => SetValue(SecondTextProperty, value);
    }

    public static readonly DependencyProperty SecondTextProperty =
        Register(nameof(SecondText), new PlatformPropertyMetadata<TimePicker, string>(defaultTimePart, OnSecondTextChanged, CoerceTimeText));

    private static void OnSecondTextChanged(TimePicker d, DependencyPropertyChangedEventArgs<string> e)
    {
        ConvertTextToTimeSpan(e.NewValue, d, t => t.Seconds, TimeSpanExtension.WithSeconds);
    }

    private static void ConvertTextToTimeSpan(string text, TimePicker timePicker, Func<TimeSpan, int> getPart, Func<TimeSpan, int, TimeSpan> convert)
    {
        if (!int.TryParse(text, out var timeSpanPart))
        {
            return;
        }

        var timeSpan = timePicker.SelectedTime;
        if (getPart(timeSpan) == timeSpanPart)
        {
            return;
        }

        timePicker.SelectedTime = convert(timeSpan, timeSpanPart);
    }

    private static string CoerceTimeText(TimePicker d, string baseValue)
    {
        if (baseValue.IsNullOrWhiteSpace() || baseValue.All(c => c == '0'))
        {
            return defaultTimePart;
        }

        return baseValue;
    }

    #endregion

}
