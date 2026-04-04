using CelestialMapper.Core.Astronomy;
using PracticalAstronomy.CSharp;
using System.Windows;
using System.Windows.Media;

namespace CelestialMapper.UI;

public class ConstellationUIElement : UIElement
{
    public required Constellation Constellation { get; init; }
    public required double MapDiameter { get; init; }

    private double Radius => MapDiameter / 2d;
    private Point Center => new(Radius, Radius);

    protected override void OnRender(DrawingContext dc)
    {
        var pen = new Pen(Brushes.Red, 0.5);

        foreach (var line in Constellation.ConstellationLines)
        {
            var segment = CreateClippedSegment(line);
            if (segment is null)
                continue;

            dc.DrawLine(pen, segment.Value.Start, segment.Value.End);
        }
    }

    private (Point Start, Point End)? CreateClippedSegment(ConstellationLine line)
    {
        Point? start = ToMapPoint(line.Start);
        Point? end = ToMapPoint(line.Stop);

        var startInside = IsInsideMap(start.Value);
        var endInside = IsInsideMap(end.Value);

        // Completely outside → skip early
        if (!startInside && !endInside)
            return null;

        if (!startInside)
        {
            start = FindIntersection(end.Value, start.Value);
            if (start is null)
            {
                return null;
            }
        }

        if (!endInside)
        {
            end = FindIntersection(start.Value, end.Value);
            if (end is null)
            {
                return null;
            }
        }

        return (start.Value, end.Value);
    }

    private Point ToMapPoint(Horizon coords)
    {
        return AstronomyCoordsHelper
            .MapCartesianCoords(coords, MapDiameter)
            .ToPoint()
            .Add(Radius);
    }

    private bool IsInsideMap(Point p)
    {
        var dx = p.X - Center.X;
        var dy = p.Y - Center.Y;

        return (dx * dx + dy * dy) < (Radius * Radius);
    }

    private Point? FindIntersection(Point from, Point to)
    {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;

        var fx = from.X - Center.X;
        var fy = from.Y - Center.Y;

        var a = dx * dx + dy * dy;
        var b = 2 * (fx * dx + fy * dy);
        var c = fx * fx + fy * fy - Radius * Radius;

        var discriminant = b * b - 4 * a * c;

        if (a <= double.Epsilon || discriminant < 0)
            return null;

        var sqrt = Math.Sqrt(discriminant);

        // We only need ONE valid intersection toward "to"
        var t = (-b + sqrt) / (2 * a);

        return new Point(
            from.X + t * dx,
            from.Y + t * dy
        );
    }
}