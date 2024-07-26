using System.Globalization;
using System.Windows.Data;

namespace CelestialMapper.UI;

public class ConverterGroup<TFrom, TTo, TParameter> : ValueConverterBase<TFrom, TTo, TParameter>
    where TFrom : notnull
    where TTo : notnull
{

    public ConverterGroup()
    {
        
    }

    public ConverterGroup(params IValueConverter[] converters)
    {
        Converters.AddRange(converters);
    }

    public List<IValueConverter> Converters { get; } = new();

    public override TTo Convert(TFrom value, Type targetType, TParameter parameter, CultureInfo culture)
    {
        object? result = value;
        foreach (var converter in Converters)
        {
            result = converter.Convert(result, targetType, parameter, culture);
        }
        return (TTo)result;
    }

    public override TFrom ConvertBack(TTo value, Type targetType, TParameter parameter, CultureInfo culture)
    {
        object? result = value;
        foreach (var converter in Converters.Reverse<IValueConverter>())
        {
            result = converter.ConvertBack(result, targetType, parameter, culture);
        }
        return (TFrom)result;
    }
}
