namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public abstract class FeatureViewBase : PlatformUserControl
{

    public FeatureViewBase()
        : base()
    {
    }

    public FeatureViewBase(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider)
    {
        AllowInitializeComponent = allowInitializeComponent;
    }

    internal bool AllowInitializeComponent { get; }

    protected abstract Type ViewModelType { get; }

    public abstract FeatureName DefaultFeatureName { get; }

    public FeatureName FeatureName
    {
        get => this.GetValue<FeatureName>(FeatureNameProperty);
        set => SetValue(FeatureNameProperty, value);
    }

    public static readonly DependencyProperty FeatureNameProperty =
        Register(nameof(FeatureName), new PlatformPropertyMetadata<FeatureViewBase, FeatureName>(FeatureName.Unknown));

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        InitializeViewModel();
    }

    public virtual void InitializeViewModel()
    {
        DataContext = ServiceProvider.ResolveViewModel(
            ViewModelType,
            FeatureName.IsUnknown()
            ? DefaultFeatureName
            : FeatureName);
    }

}
