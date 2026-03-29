using System.Globalization;
using System.Windows;

namespace CelestialMapper.UI;

public class TextDecorationsConverter : ValueConverterBase<(bool IsUnderline, bool IsStrikethrough), TextDecorationCollection, object>
{
    public override OnWrongType OnWrongType => OnWrongType.ThrowException;

    public override TextDecorationCollection Convert((bool IsUnderline, bool IsStrikethrough) value, Type targetType, object? parameter, CultureInfo culture)
    {
        var decorations = new TextDecorationCollection();

        if (value.IsUnderline)
        {
            decorations.Add(TextDecorations.Underline);
        }

        if (value.IsStrikethrough)
        {
            decorations.Add(TextDecorations.Strikethrough);
        }

        return decorations;
    }

    public override (bool IsUnderline, bool IsStrikethrough) ConvertBack(TextDecorationCollection value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool isUnderline = value?.Any(d => d.Location == TextDecorationLocation.Underline) ?? false;
        bool isStrikethrough = value?.Any(d => d.Location == TextDecorationLocation.Strikethrough) ?? false;
        return (isUnderline, isStrikethrough);
    }
}
