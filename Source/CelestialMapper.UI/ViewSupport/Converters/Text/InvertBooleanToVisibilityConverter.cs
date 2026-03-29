using System.Globalization;
using System.Windows;

namespace CelestialMapper.UI;

public class InvertBooleanToVisibilityConverter : ValueConverterBase<bool, Visibility, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override Visibility Convert(bool value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value ? Visibility.Collapsed : Visibility.Visible;
    }

    public override bool ConvertBack(Visibility value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != Visibility.Visible;
    }
}
