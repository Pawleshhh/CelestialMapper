namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public abstract class FeatureViewBase : PlatformUserControl
{

    public FeatureViewBase()
        : base()
    {
    }

    public FeatureViewBase(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    protected abstract Type ViewModelType { get; }

    public abstract string DefaultFeatureName { get; }

    public string FeatureName
    {
        get => this.GetValue<string>(FeatureNameProperty);
        set => SetValue(FeatureNameProperty, value);
    }

    public static readonly DependencyProperty FeatureNameProperty =
        Register<string, FeatureViewBase>(nameof(FeatureName), new(string.Empty));

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        InitializeViewModel();
    }

    public virtual void InitializeViewModel()
    {
        DataContext = ServiceProvider.ResolveViewModel(
            ViewModelType,
            FeatureName.IsNullOrEmpty()
            ? DefaultFeatureName
            : FeatureName);
    }

}
