using CelestialMapper.Core.Astronomy;
using System.Windows.Media;

namespace CelestialMapper.UI;

public class ConstellationUIElement : UIElement
{

    public required Constellation Constellation { get; init; }

    public required double MapDiameter { get; init; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var linePen = new Pen(Brushes.Red, 0.5);
        var mapRadius = MapDiameter / 2d;
        var center = new Point(mapRadius, mapRadius);
        foreach (var line in Constellation.ConstellationLines)
        {
            var startCartesian = AstronomyCoordsHelper.MapCartesianCoords(line.Start, MapDiameter)
                .ToPoint()
                .Add(mapRadius);
            var stopCartesian = AstronomyCoordsHelper.MapCartesianCoords(line.Stop, MapDiameter)
                .ToPoint()
                .Add(mapRadius);

            var startOnMap = PointOnMap(startCartesian, center, mapRadius);
            var stopOnMap = PointOnMap(stopCartesian, center, mapRadius);

            if (!startOnMap && !stopOnMap)
            {
                continue;
            }

            if (!startOnMap)
            {
                var intersection = IntersectionWithCircle(stopCartesian, startCartesian, center, mapRadius);

                if (intersection is null)
                {
                    continue;
                }

                startCartesian = intersection.Value;
            }
            if (!stopOnMap)
            {
                var intersection = IntersectionWithCircle(startCartesian, stopCartesian, center, mapRadius);

                if (intersection is null)
                {
                    continue;
                }

                stopCartesian = intersection.Value;
            }

            drawingContext.DrawLine(linePen, startCartesian, stopCartesian);
        }
    }

    private static bool PointOnMap(Point p, Point center, double mapRadius)
    {
        var (h, k) = (center.X, center.Y);
        var (x, y) = (p.X, p.Y);
        var distance = MathHelper.LineLength(x, y, h, k);

        return distance < mapRadius;
    }

    private static Point? IntersectionWithCircle(Point pointA, Point pointB, Point center, double mapRadius)
    {
        double dx, dy, A, B, C, det, t;
        var (cx, cy) = (center.X, center.Y);
        var (x1, y1) = (pointA.X, pointA.Y);
        var (x2, y2) = (pointB.X, pointB.Y);

        dx = x2 - x1;
        dy = y2 - y1;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (x1 - cx) + dy * (y1 - cy));
        C = (x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) - mapRadius * mapRadius;

        det = B * B - 4 * A * C;
        if ((A <= Math.E) || (det < 0))
        {
            return null;
        }
        else if (det == 0)
        {
            t = -B / (2 * A);
            var intersection1 = new Point(x1 + t * dx, y1 + t * dy);
            return intersection1;
        }
        else
        {
            t = ((-B + Math.Sqrt(det)) / (2 * A));
            var intersection1 = new Point(x1 + t * dx, y1 + t * dy);

            return intersection1;
        }
    }

}
