﻿namespace CelestialMapper.UI;

using CelestialMapper.Core.Astronomy;
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
        Register<IReadOnlySet<CelestialObject>, CelestialMap>(
            nameof(CelestialObjects), 
            new(null, OnCelestialObjectsChanged));

    private static void OnCelestialObjectsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (!CanHandle<CelestialMap, IReadOnlySet<CelestialObject>>(d, e, out var celestialMap, out var _))
        {
            return;
        }

        celestialMap.UpdateMapCanvas();
    }

    public void UpdateMapCanvas()
    {
        var mapDiameter = Diameter;
        var canvasPoint = new Point(mapDiameter / 2d, mapDiameter / 2d);

        foreach (var celestialObject in CelestialObjects)
        {
            var size = CelestialObjectUIElement.GetSize(celestialObject.Magnitude);
            var celestialObjectUI = new CelestialObjectUIElement
            {
                Size = size
            };

            var (x, y) = AstronomyCoordsHelper.MapCartesianCoords(
                celestialObject.HorizonCoordinates,
                mapDiameter,
                celestialObjectUI.Size);

            celestialObjectUI.Position = new(x, y);
            celestialObjectUI.CanvasPoint = canvasPoint;

            this.mapCanvas.Children.Add(celestialObjectUI);
        }
    }

    #endregion

}
