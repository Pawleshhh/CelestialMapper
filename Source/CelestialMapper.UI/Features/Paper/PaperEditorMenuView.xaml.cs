using CelestialMapper.Common;

namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for PaperEditorMenuView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(PaperEditorMenuView), IsSingleton = false, Key = nameof(PaperEditorMenuView), IsKeyed = true)]
public partial class PaperEditorMenuView : FeatureViewBase
{

    public PaperEditorMenuView()
    {
        InitializeComponent();
    }

    public PaperEditorMenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperEditorMenu;

    protected override Type ViewModelType => typeof(PaperEditorMenuViewModel);

}
