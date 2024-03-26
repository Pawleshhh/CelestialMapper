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
                var intersection = IntersectionWithCircle(startCartesian, stopCartesian, center, mapRadius);

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

    private static bool LineOnMap(Point a, Point b, Point center, double mapRadius)
    {
        return PointOnMap(a, center, mapRadius)
            || PointOnMap(b, center, mapRadius);
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
        double baX = pointB.X - pointA.X;
        double baY = pointB.Y - pointA.Y;
        double caX = center.X - pointA.X;
        double caY = center.Y - pointA.Y;

        double a = baX * baX + baY * baY;
        double bBy2 = baX * caX + baY * caY;
        double c = caX * caX + caY * caY - mapRadius * mapRadius;

        double pBy2 = bBy2 / a;
        double q = c / a;

        double disc = pBy2 * pBy2 - q;
        if (disc < 0)
        {
            return null;
        }

        double tmpSqrt = Math.Sqrt(disc);
        double abScalingFactor1 = -pBy2 + tmpSqrt;
        double abScalingFactor2 = -pBy2 - tmpSqrt;

        Point p1 = new Point(pointA.X - baX * abScalingFactor1, pointA.Y
                - baY * abScalingFactor1);

        return p1;
    }

}
