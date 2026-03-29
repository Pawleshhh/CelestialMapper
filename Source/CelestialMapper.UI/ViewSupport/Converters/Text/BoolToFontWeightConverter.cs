using System.Globalization;
using System.Windows;

namespace CelestialMapper.UI;

public class BoolToFontWeightConverter : ValueConverterBase<bool, FontWeight, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override FontWeight Convert(bool value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value ? FontWeights.Bold : FontWeights.Normal;
    }

    public override bool ConvertBack(FontWeight value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == FontWeights.Bold;
    }
}
