using System.Collections.ObjectModel;
using System.Reflection;

namespace CelestialMapper.ViewModel;

[Export(typeof(PaperItemsCollectionViewModel), IsSingleton = false, Key = nameof(PaperItemsCollectionViewModel))]
public class PaperItemsCollectionViewModel : ViewModelBase
{

    private readonly IPaperEditor paperEditor;

    public PaperItemsCollectionViewModel(
        IViewModelSupport viewModelSupport,
        IPaperEditor paperEditor) : base(viewModelSupport)
    {
        this.paperEditor = paperEditor;
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperItemsCollection;

    public ObservableCollection<PaperItemCategoryAdder> ItemAdders { get; } = new();

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        InitializeItemAdders();
    }

    private void InitializeItemAdders()
    {
        var paperItemIdentifiers = GetType().Assembly
            .GetTypes()
            .SelectMany(type => type.GetCustomAttributes<PaperItemIdentifierAttribute>(false));

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
