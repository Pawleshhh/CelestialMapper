namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public abstract class FeatureViewBase : PlatformUserControl
{

    public FeatureViewBase()
        : base()
    {
        Loaded += FeatureViewBase_Loaded;
        Unloaded += FeatureViewBase_Unloaded;
    }

    public FeatureViewBase(IServiceProvider serviceProvider, bool allowInitializeComponent = false)
        : base(serviceProvider)
    {
        Loaded += FeatureViewBase_Loaded;
        Unloaded += FeatureViewBase_Unloaded;
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

    public bool DoNotInitializeViewModel
    {
        get { return (bool)GetValue(DoNotInitializeViewModelProperty); }
        set { SetValue(DoNotInitializeViewModelProperty, value); }
    }

    public static readonly DependencyProperty DoNotInitializeViewModelProperty =
        Register(nameof(DoNotInitializeViewModel), new PlatformPropertyMetadata<FeatureViewBase, bool>(false));

    private void FeatureViewBase_Loaded(object sender, RoutedEventArgs e)
    {
        OnLoaded();
    }

    private void FeatureViewBase_Unloaded(object sender, RoutedEventArgs e)
    {
        Loaded -= FeatureViewBase_Loaded;
        Unloaded -= FeatureViewBase_Unloaded;
        OnUnloaded();
    }

    protected virtual void OnLoaded()
    {
        InitializeViewModel();
    }

    protected virtual void OnUnloaded() { }

    public abstract void InitializeView();

    public virtual void InitializeViewModel()
    {
        if (DoNotInitializeViewModel)
        {
            return;
        }

        DataContext = ServiceProvider.ResolveViewModel(
            ViewModelType,
            FeatureName.IsUnknown()
            ? DefaultFeatureName
            : FeatureName);
    }

}
