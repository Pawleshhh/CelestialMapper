namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapView.xaml
/// </summary>
public partial class MenuView : FeatureViewBase
{

    public MenuView()
    {
        InitializeComponent();
    }

    public MenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override string DefaultFeatureName => FeatureNames.Menu;

    protected override Type ViewModelType => typeof(MenuViewModel);

}
