namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public class LabelExtension
{

    public static TextPlacement GetLabelPlacement(DependencyObject obj)
    {
        return (TextPlacement)obj.GetValue(LabelPlacementProperty);
    }

    public static void SetLabelPlacement(DependencyObject obj, TextPlacement value)
    {
        obj.SetValue(LabelPlacementProperty, value);
    }

    public static readonly DependencyProperty LabelPlacementProperty =
        RegisterAttached<TextPlacement, LabelExtension>(
            "LabelPlacement", 
            new PlatformFrameworkPropertyMetadata<DependencyObject, TextPlacement>(TextPlacement.Left, FrameworkPropertyMetadataOptions.Inherits));

    public static string GetLabelText(DependencyObject obj)
    {
        return (string)obj.GetValue(LabelTextProperty);
    }

    public static void SetLabelText(DependencyObject obj, string value)
    {
        obj.SetValue(LabelTextProperty, value);
    }

    public static readonly DependencyProperty LabelTextProperty =
        RegisterAttached<string, LabelExtension>(
            "LabelText",
            new PlatformFrameworkPropertyMetadata<DependencyObject, string>(string.Empty, FrameworkPropertyMetadataOptions.Inherits));

    public static Style GetLabelStyle(DependencyObject obj)
    {
        return (Style)obj.GetValue(LabelStyleProperty);
    }

    public static void SetLabelStyle(DependencyObject obj, Style value)
    {
        obj.SetValue(LabelStyleProperty, value);
    }

    public static readonly DependencyProperty LabelStyleProperty =
        RegisterAttached<Style, LabelExtension>(
            "LabelStyle",
            new PlatformFrameworkPropertyMetadata<DependencyObject, Style>(null, FrameworkPropertyMetadataOptions.Inherits));

}
