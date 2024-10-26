namespace CelestialMapper.ViewModel;

using System.Collections.ObjectModel;

[Export(typeof(PaperViewModel), IsSingleton = false, Key = nameof(PaperViewModel))]
public class PaperViewModel : ViewModelBase
{

    private readonly IPaperEditor paperEditor;

    #region Constructor

    public PaperViewModel(
        IPaperEditor paperEditor,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.paperEditor = paperEditor;
    }

    #endregion

    #region ViewModelBase

    public override FeatureName DefaultFeatureName => FeatureNames.Paper;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        PaperItems = new();
        this.paperEditor.AddPaperItem(PaperItemType.Map);
        this.paperEditor.AddPaperItem(PaperItemType.Text, "Hello World");
    }

    public override Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators()
    {
        return new()
        {
            [DefaultFeatureName] = IViewModelConfigurator.Create(DefaultFeatureName, GetSubViewModels)
        };

        IEnumerable<IViewModel> GetSubViewModels()
        {
            yield return GetViewModel<MapViewModel>(FeatureNames.Map);
        }
    }

    protected override void SubscribeToEvents()
    {
        this.paperEditor.PaperItemAdded += PaperEditor_PaperItemAdded;
        this.paperEditor.PaperItemRemoved += PaperEditor_PaperItemRemoved; 
    }

    protected override void UnsubscribeFromEvents()
    {
        this.paperEditor.PaperItemAdded -= PaperEditor_PaperItemAdded;
        this.paperEditor.PaperItemRemoved -= PaperEditor_PaperItemRemoved;
    }

    #endregion

    #region Commands

    #endregion

    #region Properties

    public ObservableCollection<IPaperItem> PaperItems
    {
        get => GetPropertyValue<ObservableCollection<IPaperItem>>()!;
        private set => SetPropertyValue(value);
    }

    #endregion

    private void PaperEditor_PaperItemAdded(IPaperEditor sender, PlatformEventArgs<IPaperItem> e)
    {
        if (e?.Data is null)
        {
            return;
        }

        PaperItems.Add(e.Data);
    }

    private void PaperEditor_PaperItemRemoved(IPaperEditor sender, PlatformEventArgs<IPaperItem> e)
    {
        if (e?.Data is null)
        {
            return;
        }

        for (int i = 0; i < PaperItems.Count; i++)
        {
            if (PaperItems[i].Id == e.Data.Id)
            {
                PaperItems.RemoveAt(i);
                break;
            }
        }
    }

}
