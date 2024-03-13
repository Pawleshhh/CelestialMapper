namespace CelestialMapper.UI;

using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using static CelestialMapper.UI.DependencyPropertyHelper;

/// <summary>
/// Interaction logic for CelestialMap.xaml
/// </summary>
public partial class CelestialMap : UserControl
{

    #region Fields

    #endregion

    public CelestialMap()
    {
        InitializeComponent();

        UpdateAzimuthLines();
        UpdateAltitudeLines();
    }

    #region Properties

    public double Diameter => (double)FindResource("Double.Map.Diameter");

    #endregion

    #region GridLines

    public double[] AltitudeAngles => new[] { 30d, 60d, 85d };

    public double[] AzimuthAngles => Enumerable.Range(0, 12).Select(i => 30d * i).ToArray();

    private void UpdateAltitudeLines()
    {
        this.altitudeLines.Children.Clear();

        var diameter = Diameter;
        var altitudeAngels = AltitudeAngles;
        for (int i = 0; i < altitudeAngels.Length; i++)
        {
            var currentDiameter = GetAltitudeEllipseDiameter(diameter, altitudeAngels[i]);
            var ellipse = CreateEllipse(currentDiameter);
            this.altitudeLines.Children.Add(ellipse);
        }

        static Ellipse CreateEllipse(double diameter)
            => new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = diameter,
                Width = diameter,
                Fill = null,
                Stroke = Brushes.Red
            };
    }

    private double GetAltitudeEllipseDiameter(double diameter, double altitude)
    {
        const double maxAltitude = 90d;
        double percent = (maxAltitude - altitude) / maxAltitude;
        return diameter * percent;
    }

    private void UpdateAzimuthLines()
    {
        this.azimuthLines.Children.Clear();

        var diameter = Diameter;
        var azimuthAngles = AzimuthAngles;
        var altitude80Diameter = GetAltitudeEllipseDiameter(diameter, AltitudeAngles.Last());
        var lineLength = (diameter / 2d) - (altitude80Diameter / 2d);
        for (int i = 0; i < azimuthAngles.Length; i++)
        {
            var rectangle = CreateLine(lineLength);
            rectangle.RenderTransform = RenderTransformHelper.CreateTransformGroup(
                Rotate.By(azimuthAngles[i]),
                Translate.To(0, -lineLength / 2d - (altitude80Diameter / 2d)));
            this.azimuthLines.Children.Add(rectangle);
        }

        static Rectangle CreateLine(double length)
            => new()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = length,
                StrokeThickness = 1,
                Fill = Brushes.Green,
                Stroke = Brushes.Green,
                RenderTransformOrigin = new(0.5, 0.5)
            };
    }

    #endregion

}
