namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public enum ResizeDirection
{
    TopRight,
    BottomRight,
    BottomLeft,
    TopLeft,
}

public class ResizeButton : Button
{

    public ResizeDirection ResizeDirection
    {
        get => this.GetValue<ResizeDirection>(ResizeDirectionProperty);
        set => SetValue(ResizeDirectionProperty, value);
    }

    public static readonly DependencyProperty ResizeDirectionProperty =
        Register(nameof(ResizeDirection), new PlatformPropertyMetadata<ResizeButton, ResizeDirection>(ResizeDirection.TopRight));

}
