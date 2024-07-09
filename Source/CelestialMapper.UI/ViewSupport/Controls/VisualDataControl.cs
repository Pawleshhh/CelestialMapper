namespace CelestialMapper.UI;

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

        if (newContent is IVisualData visualData)
        {
            visualData.IsVisible = Visibility == Visibility.Visible;
            visualData.Height = Height;
            visualData.Width = Width;
        }
    }
}
