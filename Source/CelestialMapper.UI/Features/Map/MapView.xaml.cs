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

    public MapView(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        InitializeComponent();
    }

    public override string DefaultFeatureName => FeatureNames.Map;

    protected override Type ViewModelType => typeof(MapViewModel);

}
