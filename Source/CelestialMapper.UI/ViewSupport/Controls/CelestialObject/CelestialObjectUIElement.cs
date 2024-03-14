namespace CelestialMapper.UI;

using System.Windows.Media;

public class CelestialObjectUIElement : UIElement
{

    public required Point Position { get; init; }

    public required double Size { get; init; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        drawingContext.DrawEllipse(Brushes.Red, null, Position, Size / 2d, Size / 2d);
    }
}
