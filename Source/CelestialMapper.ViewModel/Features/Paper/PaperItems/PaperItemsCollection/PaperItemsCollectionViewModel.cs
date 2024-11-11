using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

[Export(typeof(PaperItemsCollectionViewModel), IsSingleton = false, Key = nameof(PaperItemsCollectionViewModel))]
public class PaperItemsCollectionViewModel : ViewModelBase, IMenuItemViewModel
{

    private readonly IPaperEditor paperEditor;
    private readonly IAttributeRetrievalService attributeRetrievalService;

    public PaperItemsCollectionViewModel(
        IViewModelSupport viewModelSupport,
        IPaperEditor paperEditor,
        IAttributeRetrievalService attributeRetrievalService) : base(viewModelSupport)
    {
        this.paperEditor = paperEditor;
        this.attributeRetrievalService = attributeRetrievalService;
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperItemsCollection;

    public ObservableCollection<PaperItemCategoryAdder> ItemAdders { get; } = new();
    public bool IsAvailable
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
        IsAvailable = true;

        InitializeItemAdders();
    }

    private void InitializeItemAdders()
    {
        var paperItemIdentifiers = 
            this.attributeRetrievalService.GetAttributes<PaperItemIdentifierAttribute>(GetType().Assembly);

        foreach (var itemIdentifier in paperItemIdentifiers)
        {
            var categoryAdder = ItemAdders.FirstOrDefault(x => x.Category == itemIdentifier.Category);
            if (categoryAdder is null)
            {
                categoryAdder = new PaperItemCategoryAdder(this.paperEditor)
                {
                    Category = itemIdentifier.Category
                };

                ItemAdders.Add(categoryAdder);
            }

            categoryAdder.PaperItemAdders.Add(new PaperItemAdder(this.paperEditor)
            {
                PaperItemType = itemIdentifier.ItemType
            });
        }
    }
}
