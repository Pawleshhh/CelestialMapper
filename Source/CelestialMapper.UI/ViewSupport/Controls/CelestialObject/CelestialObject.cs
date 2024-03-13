namespace CelestialMapper.UI;

using System.Windows.Media;
using static CelestialMapper.UI.DependencyPropertyHelper;
using CelestialObjectData = Core.Astronomy.CelestialObject;

public class CelestialObject : UIElement
{

    public CelestialObjectData CelestialObjectData
    {
        get { return this.GetValue<CelestialObjectData>(CelestialObjectDataProperty); }
        set { SetValue(CelestialObjectDataProperty, value); }
    }

    public static readonly DependencyProperty CelestialObjectDataProperty =
        Register<CelestialObjectData, CelestialObject>(nameof(CelestialObjectData));

    protected override void OnRender(DrawingContext drawingContext)
    {
        var bigObject = NumRange.Of(double.MinValue, -1d);
        var mediumObject = NumRange.Of(-0.999d, 1d);
        var smallObject = NumRange.Of(1.001d, 4d);
        var verySmallObject = NumRange.Of(4.001, double.MaxValue);

        double radius = CelestialObjectData.Magnitude switch
        {
            var m when verySmallObject.InRange(m) => 0.25,
            var m when smallObject.InRange(m) => 0.5,
            var m when mediumObject.InRange(m) => 1,
            var m when bigObject.InRange(m) => 2,
            _ => throw new InvalidOperationException()
        };

        drawingContext.DrawEllipse(Brushes.Yellow, null, new(), radius, radius);
    }

}
