namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapView.xaml
/// </summary>
public partial class MapView : FeatureViewBase
{

    public MapView()
    {
        InitializeComponent();
    }

    public MapView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.Map;

    protected override Type ViewModelType => typeof(MapViewModel);

}
