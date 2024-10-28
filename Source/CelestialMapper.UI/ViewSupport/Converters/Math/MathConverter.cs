using System.Globalization;
using System.Numerics;

namespace CelestialMapper.UI;

public class MathConverter<T> : ValueConverterBase<T?, T?, T?>
    where T : struct, INumber<T>
{

    public T? Parameter { get; set; }

    protected virtual Func<T?, T?, T?> ConvertValue { get; } = (v, p) => v;

    protected virtual Func<T?, T?, T?> ConvertValueBack { get; } = (v, p) => v;

    public override T? Convert(T? value, Type targetType, T? parameter, CultureInfo culture)
    {
        if (Parameter is not null)
        {
            parameter = Parameter;
        }

        return ConvertValue(value, parameter);
    }

    public override T? ConvertBack(T? value, Type targetType, T? parameter, CultureInfo culture)
    {
        if (Parameter is not null)
        {
            parameter = Parameter;
        }

        return ConvertValueBack(value, parameter);
    }
}
