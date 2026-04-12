using CelestialMapper.Common;

namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapEditorPropertiesMenuView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(MapEditorPropertiesMenuView), IsSingleton = false, Key = nameof(MapEditorPropertiesMenuView), IsKeyed = true)]
public partial class MapEditorPropertiesMenuView : FeatureViewBase
{

    public MapEditorPropertiesMenuView()
    {
        InitializeView();
    }

    public MapEditorPropertiesMenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditorPropertiesMenu;

    protected override Type ViewModelType => typeof(MapEditorPropertiesMenuViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
