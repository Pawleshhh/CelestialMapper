using System.Globalization;

namespace CelestialMapper.UI;

public class BoolToTextWrapping : ValueConverterBase<bool, TextWrapping, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override TextWrapping Convert(bool value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }

    public override bool ConvertBack(TextWrapping value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == TextWrapping.Wrap;
    }
}