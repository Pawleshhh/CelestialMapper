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

    private bool isPanning = false;
    private Point lastMousePosition = new();

    private readonly CelestialUIObjectSelectionHelper selectionHelper;

    #endregion

    public CelestialMap()
    {
        InitializeComponent();

        this.selectionHelper = new(OnSelectionChanged);

        UpdateAzimuthLines();
        UpdateAltitudeLines();

        DataContextChanged += CelestialMap_DataContextChanged;
        Unloaded += CelestialMap_Unloaded;
        PreviewMouseWheel += CelestialMap_PreviewMouseWheel;
        MouseLeftButtonDown += CelestialMap_MouseLeftButtonDown;
        MouseLeftButtonUp += CelestialMap_MouseLeftButtonUp;
        MouseMove += CelestialMap_MouseMove;

#if DEBUG
        InitializeDebug();
#endif
    }

    private void CelestialMap_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        UpdateCelestialObjects();
        UpdateConstellations();
    }

    private void CelestialMap_Unloaded(object sender, RoutedEventArgs e)
    {
        DataContextChanged -= CelestialMap_DataContextChanged;
        PreviewMouseWheel -= CelestialMap_PreviewMouseWheel;
        MouseLeftButtonDown -= CelestialMap_MouseLeftButtonDown;
        MouseLeftButtonUp -= CelestialMap_MouseLeftButtonUp;
        MouseMove -= CelestialMap_MouseMove;
    }

    private void CelestialMap_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (!IsZoomEnabled)
        {
            return;
        }

        e.Handled = true;

        var zoomDirection = e.Delta > 0 ? 1 : -1;
        var newZoomLevel = CurrentZoomLevel + (ZoomSensitivity * zoomDirection);
        newZoomLevel = Math.Clamp(newZoomLevel, MinZoomLevel, MaxZoomLevel);

        if (Math.Abs(newZoomLevel - CurrentZoomLevel) < 0.001d)
        {
            return;
        }

        CurrentZoomLevel = newZoomLevel;
        UpdateMainGridTransform();

        // Reset pan position when zoom returns to 1.0
        if (Math.Abs(CurrentZoomLevel - 1.0d) < 0.001d)
        {
            ResetPanPosition();
        }
    }

    private void CelestialMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!IsPanEnabled || CurrentZoomLevel <= 1.0d)
        {
            return;
        }

        this.isPanning = true;
        this.lastMousePosition = e.GetPosition(this);
        this.CaptureMouse();
    }

    private void CelestialMap_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.isPanning = false;
        this.ReleaseMouseCapture();
    }

    private void CelestialMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!this.isPanning)
        {
            return;
        }

        var currentMousePosition = e.GetPosition(this);
        var delta = currentMousePosition - this.lastMousePosition;

        PanX += delta.X;
        PanY += delta.Y;

        this.lastMousePosition = currentMousePosition;
    }

    private void UpdateMainGridTransform()
    {
        this.scaleTransform.ScaleX = -CurrentZoomLevel;
        this.scaleTransform.ScaleY = -CurrentZoomLevel;
    }

    private void ResetPanPosition()
    {
        PanX = 0;
        PanY = 0;
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

    public double CurrentZoomLevel
    {
        get { return this.GetValue<double>(CurrentZoomLevelProperty); }
        set { SetValue(CurrentZoomLevelProperty, value); }
    }

    public static readonly DependencyProperty CurrentZoomLevelProperty =
        Register(
            nameof(CurrentZoomLevel),
            new PlatformPropertyMetadata<CelestialMap, double>(1.0d, OnCurrentZoomLevelChanged));

    private static void OnCurrentZoomLevelChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<double> e)
    {
        celestialMap.UpdateMainGridTransform();
    }

    public double MinZoomLevel
    {
        get { return this.GetValue<double>(MinZoomLevelProperty); }
        set { SetValue(MinZoomLevelProperty, value); }
    }

    public static readonly DependencyProperty MinZoomLevelProperty =
        Register(
            nameof(MinZoomLevel),
            new PlatformPropertyMetadata<CelestialMap, double>(1.0d));

    public double MaxZoomLevel
    {
        get { return this.GetValue<double>(MaxZoomLevelProperty); }
        set { SetValue(MaxZoomLevelProperty, value); }
    }

    public static readonly DependencyProperty MaxZoomLevelProperty =
        Register(
            nameof(MaxZoomLevel),
            new PlatformPropertyMetadata<CelestialMap, double>(3.0d));

    public double ZoomSensitivity
    {
        get { return this.GetValue<double>(ZoomSensitivityProperty); }
        set { SetValue(ZoomSensitivityProperty, value); }
    }

    public static readonly DependencyProperty ZoomSensitivityProperty =
        Register(
            nameof(ZoomSensitivity),
            new PlatformPropertyMetadata<CelestialMap, double>(0.1d));

    public bool IsZoomEnabled
    {
        get { return this.GetValue<bool>(IsZoomEnabledProperty); }
        set { SetValue(IsZoomEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsZoomEnabledProperty =
        Register(
            nameof(IsZoomEnabled),
            new PlatformPropertyMetadata<CelestialMap, bool>(false));

    public bool IsPanEnabled
    {
        get { return this.GetValue<bool>(IsPanEnabledProperty); }
        set { SetValue(IsPanEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsPanEnabledProperty =
        Register(
            nameof(IsPanEnabled),
            new PlatformPropertyMetadata<CelestialMap, bool>(false));

    public double PanX
    {
        get { return this.GetValue<double>(PanXProperty); }
        set { SetValue(PanXProperty, value); }
    }

    public static readonly DependencyProperty PanXProperty =
        Register(
            nameof(PanX),
            new PlatformPropertyMetadata<CelestialMap, double>(0.0d, OnPanXChanged));

    private static void OnPanXChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<double> e)
    {
        var translateTransform = (TranslateTransform)celestialMap.translateTransform;
        translateTransform.X = e.NewValue;
    }

    public double PanY
    {
        get { return this.GetValue<double>(PanYProperty); }
        set { SetValue(PanYProperty, value); }
    }

    public static readonly DependencyProperty PanYProperty =
        Register(
            nameof(PanY),
            new PlatformPropertyMetadata<CelestialMap, double>(0.0d, OnPanYChanged));

    private static void OnPanYChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<double> e)
    {
        var translateTransform = (TranslateTransform)celestialMap.translateTransform;
        translateTransform.Y = e.NewValue;
    }

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

    public IReadOnlySet<CelestialObjectVisualData> CelestialObjects
    {
        get { return this.GetValue<IReadOnlySet<CelestialObjectVisualData>>(CelestialObjectsProperty); }
        set { SetValue(CelestialObjectsProperty, value); }
    }

    public static readonly DependencyProperty CelestialObjectsProperty =
        Register(
            nameof(CelestialObjects), 
            new PlatformPropertyMetadata<CelestialMap, IReadOnlySet<CelestialObjectVisualData>>(null, OnCelestialObjectsChanged));

    private static void OnCelestialObjectsChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<IReadOnlySet<CelestialObjectVisualData>> e)
    {
        celestialMap.UpdateCelestialObjects();
    }

    public void UpdateCelestialObjects()
    {
        if (CelestialObjects is null)
        {
            return;
        }

        this.celestialObjectCanvas.Children.Clear();

        var mapDiameter = Diameter;
        var mapRadius = mapDiameter / 2d;

        foreach (var celestialObjectVisualData in CelestialObjects)
        {
            var celestialObject = (CelestialObject)celestialObjectVisualData.Data;
            if (celestialObject is null)
            {
                continue;
            }
            var (x, y) = AstronomyCoordsHelper.MapCartesianCoords(
                celestialObject.HorizonCoordinates,
                mapDiameter);
            var position = new Point(x + mapRadius, y + mapRadius);

            var celestialObjectUI = new StarUIObject(celestialObjectVisualData as VisualStarData)
            {
                Position = position
            };

            this.celestialObjectCanvas.Children.Add(celestialObjectUI);
        }
    }

    #endregion

    #region CelestialObjects

    public IReadOnlySet<VisualConstellationData> Constellations
    {
        get { return this.GetValue<IReadOnlySet<VisualConstellationData>>(ConstellationsProperty); }
        set { SetValue(ConstellationsProperty, value); }
    }

    public static readonly DependencyProperty ConstellationsProperty =
        Register(
            nameof(Constellations),
            new PlatformPropertyMetadata<CelestialMap, IReadOnlySet<VisualConstellationData>>(null, OnConstellationsChanged));

    private static void OnConstellationsChanged(CelestialMap celestialMap, DependencyPropertyChangedEventArgs<IReadOnlySet<VisualConstellationData>> e)
    {
        celestialMap.UpdateConstellations();
    }

    public void UpdateConstellations()
    {
        if (Constellations is null)
        {
            return;
        }

        this.constellationCanvas.Children.Clear();

        var mapDiameter = Diameter;

        foreach (var constellation in Constellations)
        {
            var constellationUIElement = new ConstellationUIObject(constellation)
            {
                MapDiameter = mapDiameter
            };

            this.constellationCanvas.Children.Add(constellationUIElement);
        }
    }

    #endregion

    #region Selection

    public ICelestialUIObject? SelectedObject
    {
        get { return (ICelestialUIObject?)GetValue(SelectedObjectProperty); }
        set { SetValue(SelectedObjectProperty, value); }
    }

    public static readonly DependencyProperty SelectedObjectProperty =
        Register(nameof(SelectedObject), new PlatformPropertyMetadata<CelestialMap, ICelestialUIObject?>(null, OnSelectedObjectChanged));

    private static void OnSelectedObjectChanged(CelestialMap d, DependencyPropertyChangedEventArgs<ICelestialUIObject?> e)
    {

    }

    private void OnSelectionChanged()
    {
        SelectedObject = this.selectionHelper.SelectedObject;
    }

    #endregion

}
