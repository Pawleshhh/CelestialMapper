namespace CelestialMapper.UI;

using System;
using System.Windows.Media;
using System.Windows.Shapes;
using static CelestialMapper.UI.DependencyPropertyHelper;

/// <summary>
/// Interaction logic for CelestialMap.xaml
/// </summary>
public partial class CelestialMap : UserControl
{
    public CelestialMap()
    {
        InitializeComponent();

        UpdateAzimuthLines();
    }

    #region GridLines

    public int AzimuthLineCount
    {
        get => this.GetValue<int>(AzimuthLineCountProperty);
        set => SetValue(AzimuthLineCountProperty, value);
    }

    public static readonly DependencyProperty AzimuthLineCountProperty
        = Register<int, CelestialMap>(nameof(AzimuthLineCount), new(12, OnAzimuthLineCountChanged));

    private static void OnAzimuthLineCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (!CanHandle<CelestialMap, int>(d, e, out var celestialMap, out var _))
        {
            return;
        }

        celestialMap.UpdateAzimuthLines();
    }

    private void UpdateAzimuthLines()
    {
        var lineCount = AzimuthLineCount;
        var anglePerLine = 360d / lineCount;
        var currentAngle = 0d;
        for (int i = 0; i < lineCount; i++)
        {
            var rectangle = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Fill = Brushes.Red,
                Stroke = Brushes.Red,
                StrokeThickness = 0.25,
                RenderTransformOrigin = new(0.5, 0.5)
            };
            rectangle.RenderTransform = new RotateTransform(currentAngle);
            rectangle.Width = 600;
            celestialMap.lineItems.Items.Add(rectangle);
            currentAngle += anglePerLine;
        }
    }

    #endregion

}
