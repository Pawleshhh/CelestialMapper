namespace CelestialMapper.UI;

using System;
using System.Windows.Input;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class DragContainer : PlatformUserControl
{

    private const string TopLeftResizePart = "PART_TopLeftResize";
    private const string TopRightResizePart = "PART_TopRightResize";
    private const string BottomRightResizePart = "PART_BottomRightResize";
    private const string BottomLeftResizePart = "PART_BottomLeftResize";

    private Point clickPosition;
    private Point initialMousePosition;
    private Point lastMousePosition;
    private ResizeDirection currentResizeDirection;
    private const double DragThreshold = 1.0; // Threshold in pixels to start dragging

    public DragContainer()
    {
        this.PreviewMouseLeftButtonDown += DragContainer_MouseLeftButtonDown;
        this.MouseLeftButtonUp += DragContainer_MouseLeftButtonUp;
        this.MouseMove += DragContainer_MouseMove;

        this.Loaded += DragContainer_Loaded;
        this.Unloaded += DragContainer_Unloaded;
    }

    public DragContainer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        this.PreviewMouseLeftButtonDown += DragContainer_MouseLeftButtonDown;
        this.MouseLeftButtonUp += DragContainer_MouseLeftButtonUp;
        this.MouseMove += DragContainer_MouseMove;

        this.Loaded += DragContainer_Loaded;
        this.Unloaded += DragContainer_Unloaded;
    }

    private void DragContainer_Loaded(object sender, RoutedEventArgs e)
    {
        TopLeftResizeButton!.PreviewMouseLeftButtonDown += ResizeButton_MouseLeftButtonDown;
        TopRightResizeButton!.PreviewMouseLeftButtonDown += ResizeButton_MouseLeftButtonDown;
        BottomRightResizeButton!.PreviewMouseLeftButtonDown += ResizeButton_MouseLeftButtonDown;
        BottomLeftResizeButton!.PreviewMouseLeftButtonDown += ResizeButton_MouseLeftButtonDown;

        TopLeftResizeButton!.MouseLeftButtonUp += ResizeButton_MouseLeftButtonUp;
        TopRightResizeButton!.MouseLeftButtonUp += ResizeButton_MouseLeftButtonUp;
        BottomRightResizeButton!.MouseLeftButtonUp += ResizeButton_MouseLeftButtonUp;
        BottomLeftResizeButton!.MouseLeftButtonUp += ResizeButton_MouseLeftButtonUp;
    }

    private void DragContainer_Unloaded(object sender, RoutedEventArgs e)
    {
        TopLeftResizeButton!.PreviewMouseLeftButtonDown -= ResizeButton_MouseLeftButtonDown;
        TopRightResizeButton!.PreviewMouseLeftButtonDown -= ResizeButton_MouseLeftButtonDown;
        BottomRightResizeButton!.PreviewMouseLeftButtonDown -= ResizeButton_MouseLeftButtonDown;
        BottomLeftResizeButton!.PreviewMouseLeftButtonDown -= ResizeButton_MouseLeftButtonDown;

        TopLeftResizeButton!.MouseLeftButtonUp -= ResizeButton_MouseLeftButtonUp;
        TopRightResizeButton!.MouseLeftButtonUp -= ResizeButton_MouseLeftButtonUp;
        BottomRightResizeButton!.MouseLeftButtonUp -= ResizeButton_MouseLeftButtonUp;
        BottomLeftResizeButton!.MouseLeftButtonUp -= ResizeButton_MouseLeftButtonUp;
    }

    private ResizeButton? topLeftResizeButton;
    private ResizeButton? topRightResizeButton;
    private ResizeButton? bottomRightResizeButton;
    private ResizeButton? bottomLeftResizeButton;

    public ResizeButton TopLeftResizeButton => this.topLeftResizeButton ??= (Template.FindName(TopLeftResizePart, this) as ResizeButton)!;
    public ResizeButton TopRightResizeButton => this.topRightResizeButton ??= (Template.FindName(TopRightResizePart, this) as ResizeButton)!;
    public ResizeButton BottomRightResizeButton => this.bottomRightResizeButton ??= (Template.FindName(BottomRightResizePart, this) as ResizeButton)!;
    public ResizeButton BottomLeftResizeButton => this.bottomLeftResizeButton ??= (Template.FindName(BottomLeftResizePart, this) as ResizeButton)!;

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

    public bool IsResizing
    {
        get => this.GetValue<bool>(IsResizingProperty);
        set => this.SetValue(IsResizingProperty, value);
    }

    public static DependencyProperty IsResizingProperty =
        Register(nameof(IsResizing), new PlatformPropertyMetadata<DragContainer, bool>(false));

    public bool IsSelected
    {
        get => this.GetValue<bool>(IsSelectedProperty);
        set => this.SetValue(IsSelectedProperty, value);
    }

    public static DependencyProperty IsSelectedProperty =
        Register(nameof(IsSelected), new PlatformPropertyMetadata<DragContainer, bool>(false));

    #region Drag & Select

    private void DragContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        IsDragging = true;
        IsSelected = true;
        this.clickPosition = e.GetPosition(this);
        this.initialMousePosition = e.GetPosition(RelativeParent);
        this.CaptureMouse();
    }

    private void DragContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        IsDragging = false;
        IsResizing = false;
        this.ReleaseMouseCapture();
    }

    private void ResizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var button = (ResizeButton)sender;

        this.currentResizeDirection = button.ResizeDirection;
        this.lastMousePosition = e.GetPosition(RelativeParent);
        IsResizing = true;
        IsSelected = true;

        this.CaptureMouse();
    }

    private void ResizeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var button = (ResizeButton)sender;

        IsResizing = false;

        this.ReleaseMouseCapture();
    }

    private void DragContainer_MouseMove(object sender, MouseEventArgs e)
    {
        if (!this.IsMouseCaptured)
        {
            return;
        }

        PositionInfo positionInfo = this.GetPositionInfo(RelativeParent);
        Point mousePosition = e.GetPosition(RelativeParent);

        if (IsResizing)
        {
            double deltaX, deltaY;
            (deltaX, deltaY) = this.currentResizeDirection switch
            {
                ResizeDirection.TopLeft => (mousePosition.X - positionInfo.TopLeft.X, mousePosition.Y - positionInfo.TopLeft.Y),
                ResizeDirection.TopRight => (mousePosition.X - positionInfo.TopRight.X, mousePosition.Y - positionInfo.TopLeft.Y),
                ResizeDirection.BottomRight => (mousePosition.X - positionInfo.BottomRight.X, mousePosition.Y - positionInfo.BottomRight.Y),
                ResizeDirection.BottomLeft => (mousePosition.X - positionInfo.BottomLeft.X, mousePosition.Y - positionInfo.BottomLeft.Y),
                _ => (0, 0)
            };

            // Update the last mouse position for the next movement
            this.lastMousePosition = mousePosition;

            // Calculate new size and position based on the resize direction
            switch (this.currentResizeDirection)
            {
                case ResizeDirection.TopLeft:
                    Width = Math.Max(Width - deltaX, MinWidth); // Prevent negative width
                    Height = Math.Max(Height - deltaY, MinHeight); // Prevent negative height

                    if (!IsWidthAtMin())
                    {
                        XPos += deltaX;
                    }
                    if (!IsHeightAtMin())
                    {
                        YPos += deltaY;
                    }
                    break;
                case ResizeDirection.TopRight:
                    Width = Math.Max(Width + deltaX, MinWidth); // Prevent negative width
                    Height = Math.Max(Height - deltaY, MinHeight); // Prevent negative height

                    if (!IsHeightAtMin())
                    {
                        YPos += deltaY;
                    }
                    break;
                case ResizeDirection.BottomRight:
                    Width = Math.Max(Width + deltaX, MinWidth); // Prevent negative width
                    Height = Math.Max(Height + deltaY, MinHeight); // Prevent negative height
                    break;
                case ResizeDirection.BottomLeft:
                    Width = Math.Max(Width - deltaX, MinWidth); // Prevent negative width
                    Height = Math.Max(Height + deltaY, MinHeight); // Prevent negative height
                    if (!IsWidthAtMin())
                    {
                        XPos += deltaX;
                    }
                    break;
                default:
                    break;
            }

            bool IsWidthAtMin() => Width <= MinWidth;
            bool IsHeightAtMin() => Height <= MinHeight;
            return;
        }

        if (Math.Abs(mousePosition.X - this.initialMousePosition.X) <= DragThreshold &&
            Math.Abs(mousePosition.Y - this.initialMousePosition.Y) <= DragThreshold)
        {
            return;
        }

        if (IsDragging)
        {
            double newXPos = mousePosition.X - this.clickPosition.X;
            double newYPos = mousePosition.Y - this.clickPosition.Y;

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
    #endregion
}
