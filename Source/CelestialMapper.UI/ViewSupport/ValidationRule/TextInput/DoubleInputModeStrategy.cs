using System.Globalization;

namespace CelestialMapper.UI;

public class DoubleInputModeStrategy : InputModeStrategy
{
    public override InputMode Mode => InputMode.Double;

    public double MaxValue { get; set; } = double.MaxValue;

    public double MinValue { get; set; } = double.MinValue;

    public override bool IsCorrect(string text)
    {
        if (!base.IsCorrect(text))
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

    protected virtual bool CheckRange(double value)
    {
        if (MinValue > value || MaxValue < value)
        {
            return false;
        }

        return true;
    }
}
