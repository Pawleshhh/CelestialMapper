namespace CelestialMapper.UI;

using CelestialMapper.Core.Astronomy;
using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using static CelestialMapper.UI.DependencyPropertyHelper;

/// <summary>
/// Interaction logic for CelestialMap.xaml
/// </summary>
public partial class CelestialMap : PlatformUserControl
{

    #region Fields

    #endregion

    public CelestialMap()
    {
        InitializeComponent();

        UpdateAzimuthLines();
        UpdateAltitudeLines();

#if DEBUG
        InitializeDebug();
#endif
    }

    #region Debug
#if DEBUG

    private void InitializeDebug()
    {
        this.ellipseBackground.MouseEnter += DebugCanvas_MouseEnter;
        this.ellipseBackground.MouseMove += DebugCanvas_MouseMove;
        this.ellipseBackground.MouseLeave += DebugCanvas_MouseLeave;

        this.debugCanvas.Children.Add(this.coordinatesTextBlock);
    }

    private bool showCoordinates;
    private TextBlock coordinatesTextBlock = new TextBlock()
    {
        Visibility = Visibility.Collapsed,
        Background = Brushes.Black,
        Foreground = Brushes.Yellow,
        RenderTransform = new TranslateTransform()
    };

    private void DebugCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        this.showCoordinates = true;
        this.coordinatesTextBlock.Visibility = Visibility.Visible;
    }

    private void DebugCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        this.showCoordinates = false;
        this.coordinatesTextBlock.Visibility = Visibility.Collapsed;
    }

    private void DebugCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!this.showCoordinates)
        {
            return;
        }

        var relativeMousePoint = e.GetPosition(this.debugCanvas);
        var mousePoint = e.GetPosition(this.debugCanvas);
        var mapRadius = Diameter / 2d;

        this.coordinatesTextBlock.Text = 
            $"x: {relativeMousePoint.X}, y: {relativeMousePoint.Y}\n" +
            $"x+: {relativeMousePoint.X + mapRadius}, y+: {relativeMousePoint.Y + mapRadius}";
        this.coordinatesTextBlock.RenderTransform = RenderTransformHelper.CreateTransformGroup(
            Scale.By(-1, -1),
            Translate.To(mousePoint.X - 20, mousePoint.Y - 20));
    }

#endif
    #endregion

    #region Properties

    public double Diameter => (double)GetResource("Double.Map.Diameter");

    #endregion

    #region GridLines

    public double[] AltitudeAngles => new[] { 30d, 60d, 85d };

    public double[] AzimuthAngles => Enumerable.Range(0, 12).Select(i => 30d * i).ToArray();

    public void UpdateAltitudeLines()
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
                Stroke = Brushes.Green
            };
    }

    private double GetAltitudeEllipseDiameter(double diameter, double altitude)
    {
        const double maxAltitude = 90d;
        double percent = (maxAltitude - altitude) / maxAltitude;
        return diameter * percent;
    }

    public void UpdateAzimuthLines()
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
                Translate.To(0, -lineLength / 2d - (altitude80Diameter / 2d)),
                Rotate.By(azimuthAngles[i]));
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

    #region CelestialObjects

    public IReadOnlySet<CelestialObject> CelestialObjects
    {
        get { return this.GetValue<IReadOnlySet<CelestialObject>>(CelestialObjectsProperty); }
        set { SetValue(CelestialObjectsProperty, value); }
    }

    public static readonly DependencyProperty CelestialObjectsProperty =
        Register(
            nameof(CelestialObjects), 
            new PlatformPropertyMetadata<CelestialMap, IReadOnlySet<CelestialObject>>(null, OnCelestialObjectsChanged));

    private static void OnCelestialObjectsChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<IReadOnlySet<CelestialObject>> e)
    {
        celestialMap.UpdateCelestialObjects();
    }

    public void UpdateCelestialObjects()
    {
        this.celestialObjectCanvas.Children.Clear();

        var mapDiameter = Diameter;
        var mapRadius = mapDiameter / 2d;

        foreach (var celestialObject in CelestialObjects)
        {
            var (x, y) = AstronomyCoordsHelper.MapCartesianCoords(
                celestialObject.HorizonCoordinates,
                mapDiameter);
            var position = new Point(x + mapRadius, y + mapRadius);

            var size = CelestialObjectHelper.GetSizeBasedOnMagnitude(celestialObject.Magnitude);

            var celestialObjectUI = new CelestialObjectUIElement
            {
                Position = position,
                Size = size
            };

            this.celestialObjectCanvas.Children.Add(celestialObjectUI);
        }
    }

    #endregion

    #region CelestialObjects

    public IReadOnlySet<Constellation> Constellations
    {
        get { return this.GetValue<IReadOnlySet<Constellation>>(ConstellationsProperty); }
        set { SetValue(ConstellationsProperty, value); }
    }

    public static readonly DependencyProperty ConstellationsProperty =
        Register(
            nameof(Constellations),
            new PlatformPropertyMetadata<CelestialMap, IReadOnlySet<Constellation>>(null, OnConstellationsChanged));

    private static void OnConstellationsChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<IReadOnlySet<Constellation>> e)
    {
        celestialMap.UpdateConstellations();
    }

    public void UpdateConstellations()
    {
        this.constellationCanvas.Children.Clear();

        var mapDiameter = Diameter;

        foreach (var constellation in Constellations)
        {
            var constellationUIElement = new ConstellationUIElement
            {
                Constellation = constellation,
                MapDiameter = mapDiameter
            };

            this.constellationCanvas.Children.Add(constellationUIElement);
        }
    }

    #endregion

}
