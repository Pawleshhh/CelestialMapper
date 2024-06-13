namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for ExportMenuView.xaml
/// </summary>
public partial class ExportMenuView : FeatureViewBase
{

    public ExportMenuView()
    {
        InitializeComponent();
    }

    public ExportMenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override string DefaultFeatureName => FeatureNames.ExportMenu;

    protected override Type ViewModelType => typeof(ExportMenuViewModel);

}
