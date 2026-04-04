using System.Globalization;
using System.Windows.Data;

namespace CelestialMapper.UI;


/// <summary>
/// Converter that returns all available values for an enum type.
/// </summary>
public class EnumValuesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Array.Empty<object>();

        var valueType = value.GetType();
        var baseType = Nullable.GetUnderlyingType(valueType) ?? valueType;

        if (!baseType.IsEnum)
            return Array.Empty<object>();

        return Enum.GetValues(baseType).Cast<object>().ToList();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
