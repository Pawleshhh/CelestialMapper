namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapEditorView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(MapEditorView), IsSingleton = false, Key = nameof(MapEditorView), IsKeyed = true)]
public partial class MapEditorView : FeatureViewBase
{

    public MapEditorView()
    {
        InitializeView();
    }

    public MapEditorView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.MapEditor;

    protected override Type ViewModelType => typeof(MapEditorViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
