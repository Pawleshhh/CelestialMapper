namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(MapView), IsSingleton = false, Key = nameof(MapView), IsKeyed = true)]
public partial class MapView : FeatureViewBase
{

    public MapView()
    {
        InitializeView();
    }

    public MapView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.Map;

    protected override Type ViewModelType => typeof(MapViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
