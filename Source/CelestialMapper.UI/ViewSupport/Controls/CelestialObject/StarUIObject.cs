using System.Windows.Media;

namespace CelestialMapper.UI;

public class StarUIObject : CelestialUIObject<VisualStarData>
{

    private static readonly BrushConverter brushConverter = new();

    public StarUIObject(VisualStarData visualData) : base(visualData)
    {
    }

    public required Point Position { get; set; }

    public double Size => CelestialObjectHelper.GetSizeBasedOnMagnitude(VisualData.Width.Value);

    public string Color => VisualData.BackgroundColor.Value ?? Brushes.Transparent.ToString();

    protected override void OnRender(DrawingContext drawingContext)
    {
        var color = brushConverter.ConvertFromString(Color) as Brush ?? Brushes.Transparent;
        drawingContext.DrawEllipse(
            color,
            null,
            Position,
            Size / 2d,
            Size / 2d);
    }
}
