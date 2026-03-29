using System.Globalization;
using System.Windows;

namespace CelestialMapper.UI;

public class BoolToFontStyleConverter : ValueConverterBase<bool, FontStyle, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override FontStyle Convert(bool value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value ? FontStyles.Italic : FontStyles.Normal;
    }

    public override bool ConvertBack(FontStyle value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == FontStyles.Italic;
    }
}
