using System.Globalization;

namespace CelestialMapper.UI;

public class TextPlacementToDockConverter : ValueConverterBase<TextPlacement, Dock, object>
{
    public static TextPlacementToDockConverter Instance { get; } = new TextPlacementToDockConverter();

    public override bool ExpectsParameter => false;

    public override OnWrongType OnWrongType => OnWrongType.ReturnDefault;

    public override TextPlacement DefaultFromValue => TextPlacement.Left;

    public override Dock DefaultToValue => Dock.Left;

    public override Dock Convert(TextPlacement value, Type targetType, object parameter, CultureInfo culture)
    {
        return TextPlacementToDock(value);
    }

    public override TextPlacement ConvertBack(Dock value, Type targetType, object parameter, CultureInfo culture)
    {
        return DockToTextPlacement(value);
    }

    public static Dock TextPlacementToDock(TextPlacement textPlacement)
    {
        return textPlacement switch
        {
            TextPlacement.Left => Dock.Left,
            TextPlacement.Right => Dock.Right,
            TextPlacement.Top => Dock.Top,
            TextPlacement.Bottom => Dock.Bottom,
            _ => Dock.Left
        };
    }

    public static TextPlacement DockToTextPlacement(Dock dock)
    {
        return dock switch
        {
            Dock.Left => TextPlacement.Left,
            Dock.Right => TextPlacement.Right,
            Dock.Top => TextPlacement.Top,
            Dock.Bottom => TextPlacement.Bottom,
            _ => TextPlacement.Left
        };
    }
}
