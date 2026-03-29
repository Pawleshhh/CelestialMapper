using System.Globalization;
using System.Windows;
using CelestialMapper.ViewModel;

namespace CelestialMapper.UI;

public class TextHorizontalAlignmentConverter : ValueConverterBase<CelestialMapper.ViewModel.TextHorizontalAlignment, System.Windows.TextAlignment, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override System.Windows.TextAlignment Convert(CelestialMapper.ViewModel.TextHorizontalAlignment value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            CelestialMapper.ViewModel.TextHorizontalAlignment.Left => System.Windows.TextAlignment.Left,
            CelestialMapper.ViewModel.TextHorizontalAlignment.Center => System.Windows.TextAlignment.Center,
            CelestialMapper.ViewModel.TextHorizontalAlignment.Right => System.Windows.TextAlignment.Right,
            CelestialMapper.ViewModel.TextHorizontalAlignment.Justify => System.Windows.TextAlignment.Justify,
            _ => System.Windows.TextAlignment.Left
        };
    }

    public override CelestialMapper.ViewModel.TextHorizontalAlignment ConvertBack(System.Windows.TextAlignment value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            System.Windows.TextAlignment.Left => CelestialMapper.ViewModel.TextHorizontalAlignment.Left,
            System.Windows.TextAlignment.Center => CelestialMapper.ViewModel.TextHorizontalAlignment.Center,
            System.Windows.TextAlignment.Right => CelestialMapper.ViewModel.TextHorizontalAlignment.Right,
            System.Windows.TextAlignment.Justify => CelestialMapper.ViewModel.TextHorizontalAlignment.Justify,
            _ => CelestialMapper.ViewModel.TextHorizontalAlignment.Left
        };
    }
}
