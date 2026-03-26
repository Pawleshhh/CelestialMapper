namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for PaperItemsCollectionView.xaml
/// </summary>
[Export(typeof(FeatureViewBase), typeof(PaperItemsCollectionView), IsSingleton = false, Key = nameof(PaperItemsCollectionView), IsKeyed = true)]
public partial class PaperItemsCollectionView : FeatureViewBase
{

    public PaperItemsCollectionView()
    {
        InitializeView();
    }

    public PaperItemsCollectionView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperItemsCollection;

    protected override Type ViewModelType => typeof(PaperItemsCollectionViewModel);

    public override void InitializeView()
    {
        InitializeComponent();
    }
}
