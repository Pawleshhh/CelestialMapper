namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for PaperView.xaml
/// </summary>
public partial class PaperView : FeatureViewBase
{

    public PaperView()
    {
        InitializeView();
    }

    public PaperView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.Paper;

    protected override Type ViewModelType => typeof(PaperViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
