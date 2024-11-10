namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for PaperItemsCollectionView.xaml
/// </summary>
public partial class PaperItemsCollectionView : FeatureViewBase
{

    public PaperItemsCollectionView()
    {
        InitializeComponent();
    }

    public PaperItemsCollectionView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperItemsCollection;

    protected override Type ViewModelType => typeof(PaperItemsCollectionViewModel);

}
