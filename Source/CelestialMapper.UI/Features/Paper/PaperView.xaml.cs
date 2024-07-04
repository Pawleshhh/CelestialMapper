namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for PaperView.xaml
/// </summary>
public partial class PaperView : FeatureViewBase
{

    public PaperView()
    {
        InitializeComponent();
    }

    public PaperView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.Paper;

    protected override Type ViewModelType => typeof(PaperViewModel);

}
