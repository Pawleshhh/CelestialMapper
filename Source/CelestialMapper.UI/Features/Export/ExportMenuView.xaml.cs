using CelestialMapper.Common;

namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for ExportMenuView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(ExportMenuView), IsSingleton = false, Key = nameof(ExportMenuView), IsKeyed = true)]
public partial class ExportMenuView : FeatureViewBase
{

    public ExportMenuView()
    {
        InitializeView();
    }

    public ExportMenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.ExportMenu;

    protected override Type ViewModelType => typeof(ExportMenuViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
