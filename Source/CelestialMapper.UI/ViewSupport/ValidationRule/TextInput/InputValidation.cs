using System.Globalization;

namespace CelestialMapper.UI;

public abstract class InputValidation : ValidationRule<string>
{

    public abstract InputMode Mode { get; }

    public int MaxLength { get; set; } = int.MaxValue;

    public bool AllowEmptyInput { get; set; } = true;

    public override bool IsCorrect(string text)
    {
        if (!IsLengthCorrect(text.Length))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsLengthCorrect(int length)
        => length <= MaxLength;

}
