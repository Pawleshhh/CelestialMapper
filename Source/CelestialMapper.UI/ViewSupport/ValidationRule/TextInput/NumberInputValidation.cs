using System.Globalization;
using System.Numerics;

namespace CelestialMapper.UI;

public abstract class NumberInputValidation<TNumber> : InputValidation
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>, IParsable<TNumber>
{

    public TNumber MaxValue { get; set; } = TNumber.MaxValue;

    public TNumber MinValue { get; set;} = TNumber.MinValue;

    public override bool IsCorrect(string text)
    {
        if (!base.IsCorrect(text))
        {
            return false;
        }

        if (AllowEmptyInput && text.IsNullOrEmpty())
        {
            return true;
        }

        if (!TNumber.TryParse(text, CultureInfo.InvariantCulture, out TNumber? value))
        {
            return false;
        }

        if (!CheckRange(value))
        {
            return false;
        }

        return true;
    }

    protected virtual bool CheckRange(TNumber value)
    {
        if (MinValue > value || MaxValue < value)
        {
            return false;
        }

        return true;
    }
}
