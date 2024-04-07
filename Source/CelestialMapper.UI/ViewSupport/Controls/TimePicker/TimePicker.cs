namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public class TimePicker : PlatformUserControl
{

    #region Constructors

    public TimePicker()
    {
    }

    public TimePicker(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    #endregion

    #region Properties

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
                setPart(newPart.ToString());
            }
        }
    }

    public string HourText
    {
        get => this.GetValue<string>(HourTextProperty);
        set => SetValue(HourTextProperty, value);
    }

    public static readonly DependencyProperty HourTextProperty =
        Register(nameof(HourText), new PlatformPropertyMetadata<TimePicker, string>("00", OnHourTextChanged));

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
        Register(nameof(MinuteText), new PlatformPropertyMetadata<TimePicker, string>("00", OnMinuteTextChanged));

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
        Register(nameof(SecondText), new PlatformPropertyMetadata<TimePicker, string>("00", OnSecondTextChanged));

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

    #endregion

}
