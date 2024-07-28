namespace CelestialMapper.UI;

using System.ComponentModel;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class VisualDataControl : PlatformUserControl
{
    public VisualDataControl()
    {
    }

    public VisualDataControl(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public bool OverrideVisualData
    {
        get { return (bool)GetValue(OverrideVisualDataProperty); }
        set { SetValue(OverrideVisualDataProperty, value); }
    }

    public static readonly DependencyProperty OverrideVisualDataProperty =
        Register(nameof(OverrideVisualData), new PlatformPropertyMetadata<VisualDataControl, bool>(true));

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);

        AlignWithVisualDataContent(newContent, Height, Width);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        AlignWithVisualDataContent(Content, sizeInfo.NewSize.Width, sizeInfo.NewSize.Height);
    }

    private void AlignWithVisualDataContent(object content, double width, double height)
    {
        if (content is IVisualData visualData)
        {
            visualData.IsVisible = Visibility == Visibility.Visible;
            visualData.Width = width;
            visualData.Height = height;
        }
    }

    private void SubscribeToVisualDataPropertyChanged(object data)
    {
        if (data is IVisualData and INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += VisualDataBase_PropertyChanged;
        }
    }

    private void VisualDataBase_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is IVisualData visualData)
        {
            OnVisualDataChanged(visualData, e.PropertyName);
        }
    }

    protected virtual void OnVisualDataChanged(IVisualData visualData, string? propertyName)
    {
        if (propertyName == nameof(IVisualData.Height))
        {
            Height = visualData.Height;
        }
        else if (propertyName == nameof(IVisualData.Width))
        {
            Width = visualData.Width;
        }
    }
}
