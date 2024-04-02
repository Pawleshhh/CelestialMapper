using System.Globalization;

namespace CelestialMapper.UI;

public class DoubleInputModeStrategy : InputModeStrategy
{
    public override InputMode Mode => InputMode.Double;

    public bool DigitsOnly
    {
        get { return (bool)GetValue(DigitsOnlyProperty); }
        set { SetValue(DigitsOnlyProperty, value); }
    }

    public static readonly DependencyProperty DigitsOnlyProperty =
        DependencyProperty.Register(nameof(DigitsOnly), typeof(bool), typeof(DoubleInputModeStrategy), new(false));

    public double MaxValue
    {
        get { return (double)GetValue(MaxValueProperty); }
        set { SetValue(MaxValueProperty, value); }
    }

    public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register(nameof(MaxValue), typeof(double), typeof(DoubleInputModeStrategy), new(double.MaxValue));

    public double MinValue
    {
        get { return (double)GetValue(MinValueProperty); }
        set { SetValue(MinValueProperty, value); }
    }

    public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register(nameof(MinValue), typeof(double), typeof(DoubleInputModeStrategy), new(double.MinValue));

    public override bool IsCorrect(string text)
    {
        if (!base.IsCorrect(text))
        {
            return false;
        }

        if (!CheckDigitsOnly(text))
        {
            return false;
        }

        if (!double.TryParse(text, CultureInfo.InvariantCulture, out double value))
        {
            return false;
        }

        if (!CheckRange(value))
        {
            return false;
        }

        return true;
    }

    protected virtual bool CheckDigitsOnly(string text)
    {
        if (!DigitsOnly)
        {
            return true;
        }

        foreach (char c in text)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }

        return true;
    }

    protected virtual bool CheckRange(double value)
    {
        if (MinValue > value || MaxValue < value)
        {
            return false;
        }

        return true;
    }
}
