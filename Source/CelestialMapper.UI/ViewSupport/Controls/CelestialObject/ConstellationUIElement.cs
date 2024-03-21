using CelestialMapper.Core.Astronomy;
using System.Windows.Media;

namespace CelestialMapper.UI;

public class ConstellationUIElement : UIElement
{

    public required Constellation Constellation { get; init; }

    public required double MapDiameter { get; init; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var linePen = new Pen(Brushes.Pink, 1);
        var mapRadius = MapDiameter / 2d;
        foreach (var line in Constellation.ConstellationLines)
        {
            var startCartesian = AstronomyCoordsHelper.MapCartesianCoords(line.Start, MapDiameter)
                .ToPoint()
                .Add(mapRadius);
            var stopCartesian = AstronomyCoordsHelper.MapCartesianCoords(line.Stop, MapDiameter)
                .ToPoint()
                .Add(mapRadius);

            drawingContext.DrawLine(linePen, startCartesian, stopCartesian);
        }
    }
}
