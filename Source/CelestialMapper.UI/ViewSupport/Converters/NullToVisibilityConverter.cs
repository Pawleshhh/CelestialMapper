using System.Globalization;

namespace CelestialMapper.UI;

public class NullToVisibilityConverter : ValueConverterBase<object, Visibility, object>
{

    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public Visibility WhenNull { get; set; } = Visibility.Collapsed;

    public Visibility WhenNotNull { get; set; } = Visibility.Visible;

    public override Visibility Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null ? WhenNull : WhenNotNull;
    }

    public override object? ConvertBack(Visibility value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
