using CelestialMapper.Core.Astronomy;
using System.Windows.Media;

namespace CelestialMapper.UI;

public class ConstellationUIElement : UIElement
{

    public required IReadOnlyList<Constellation> Constellations { get; init; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        foreach (var line in Constellations)
        {

        }

        //drawingContext.DrawLine(new Pen(Brushes.Pink, 1), StartPoint, StopPoint);
    }
}
