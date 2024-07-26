namespace CelestialMapper.UI;

using System;
using System.Windows.Input;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class DragContainer : PlatformUserControl
{
    private Point clickPosition;
    private Point initialMousePosition;
    private const double DragThreshold = 1.0; // Threshold in pixels to start dragging

    public DragContainer()
    {
        this.MouseLeftButtonDown += DragContainer_MouseLeftButtonDown;
        this.MouseLeftButtonUp += DragContainer_MouseLeftButtonUp;
        this.MouseMove += DragContainer_MouseMove;
    }

    public DragContainer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        this.MouseLeftButtonDown += DragContainer_MouseLeftButtonDown;
        this.MouseLeftButtonUp += DragContainer_MouseLeftButtonUp;
        this.MouseMove += DragContainer_MouseMove;
    }

    public UIElement RelativeParent
    {
        get => this.GetValue<UIElement>(RelativeParentProperty);
        set => SetValue(RelativeParentProperty, value);
    }

    public static DependencyProperty RelativeParentProperty =
        Register(nameof(RelativeParent), new PlatformPropertyMetadata<DragContainer, UIElement>((UIElement?)null));

    public double XPos
    {
        get => this.GetValue<double>(XPosProperty);
        set => this.SetValue(XPosProperty, value);
    }

    public static DependencyProperty XPosProperty =
        Register(nameof(XPos), new PlatformPropertyMetadata<DragContainer, double>(0));

    public double YPos
    {
        get => this.GetValue<double>(YPosProperty);
        set => this.SetValue(YPosProperty, value);
    }

    public static DependencyProperty YPosProperty =
        Register(nameof(YPos), new PlatformPropertyMetadata<DragContainer, double>(0));

    public bool IsDragging
    {
        get => this.GetValue<bool>(IsDraggingProperty);
        set => this.SetValue(IsDraggingProperty, value);
    }

    public static DependencyProperty IsDraggingProperty =
        Register(nameof(IsDragging), new PlatformPropertyMetadata<DragContainer, bool>(false));

    public bool IsSelected
    {
        get => this.GetValue<bool>(IsSelectedProperty);
        set => this.SetValue(IsSelectedProperty, value);
    }

    public static DependencyProperty IsSelectedProperty =
        Register(nameof(IsSelected), new PlatformPropertyMetadata<DragContainer, bool>(false));

    private void DragContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        IsDragging = false;
        IsSelected = true;
        this.clickPosition = e.GetPosition(this);
        this.initialMousePosition = e.GetPosition(RelativeParent);
        this.CaptureMouse();
    }

    private void DragContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        IsDragging = false;
        this.ReleaseMouseCapture();
    }

    private void DragContainer_MouseMove(object sender, MouseEventArgs e)
    {
        if (this.IsMouseCaptured)
        {
            Point currentPosition = e.GetPosition(RelativeParent);

            if (!IsDragging)
            {
                if (Math.Abs(currentPosition.X - this.initialMousePosition.X) > DragThreshold ||
                    Math.Abs(currentPosition.Y - this.initialMousePosition.Y) > DragThreshold)
                {
                    IsDragging = true;
                }
            }

            if (IsDragging)
            {
                double newXPos = currentPosition.X - this.clickPosition.X;
                double newYPos = currentPosition.Y - this.clickPosition.Y;

                // Clamp the position to stay within the bounds of the RelativeParent
                if (RelativeParent is FrameworkElement parentElement)
                {
                    double parentWidth = parentElement.ActualWidth;
                    double parentHeight = parentElement.ActualHeight;

                    newXPos = Math.Max(0, Math.Min(newXPos, parentWidth - this.ActualWidth));
                    newYPos = Math.Max(0, Math.Min(newYPos, parentHeight - this.ActualHeight));
                }

                XPos = newXPos;
                YPos = newYPos;
            }
        }
    }
}
