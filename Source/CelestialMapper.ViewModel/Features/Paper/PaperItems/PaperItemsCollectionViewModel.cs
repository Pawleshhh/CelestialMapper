using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

[Export(typeof(PaperItemsCollectionViewModel), IsSingleton = false, Key = nameof(PaperItemsCollectionViewModel))]
public class PaperItemsCollectionViewModel : ViewModelBase
{
    public PaperItemsCollectionViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperItemsCollection;

    public ObservableCollection<object> ItemAdders { get; } = new();

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }
}
