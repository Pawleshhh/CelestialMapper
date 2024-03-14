namespace CelestialMapper.UI;

using System.Windows.Media;

public class CelestialObjectUIElement : UIElement
{

    public Point Position { get; set; }

    public Point CanvasPoint { get; set; }

    public double Size { get; set; }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var (x, y) = (Position.X, Position.Y);
        var (cx, cy) = (CanvasPoint.X, CanvasPoint.Y);

        //SetValue(Canvas.LeftProperty, x);
        //SetValue(Canvas.TopProperty, -y);

        drawingContext.DrawEllipse(Brushes.Red, null, new(x + 360, y + 360), Size / 2d, Size / 2d);
    }

    private static readonly NumRange<double> bigObject = NumRange.Of(double.MinValue, -1d);
    private static readonly NumRange<double> mediumObject = NumRange.Of(-1d, 1d);
    private static readonly NumRange<double> smallObject = NumRange.Of(1d, 4d);
    private static readonly NumRange<double> verySmallObject = NumRange.Of(4, double.MaxValue);

    public static double GetSize(double magnitude)
    {
        double objectDiameter = magnitude switch
        {
            var m when verySmallObject.InRange(m, NumRangeKind.Exclusive) => 0.25,
            var m when smallObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 0.5,
            var m when mediumObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 1,
            var m when bigObject.InRange(m, NumRangeKind.Exclusive, NumRangeKind.Inclusive) => 2,
            _ => 0
        } * 3;

        return objectDiameter;
    }

}
