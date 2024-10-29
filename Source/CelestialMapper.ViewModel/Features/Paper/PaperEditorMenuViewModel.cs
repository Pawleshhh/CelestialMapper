namespace CelestialMapper.ViewModel;

[Export(typeof(PaperEditorMenuViewModel), IsSingleton = true, Key = nameof(PaperEditorMenuViewModel))]
public class PaperEditorMenuViewModel : ViewModelBase
{

    private readonly IPaperEditor paperEditor;

    public PaperEditorMenuViewModel(
        IPaperEditor paperEditor, 
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.paperEditor = paperEditor;
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PaperEditorMenu;

    public IPaperItem? SelectedPaperItem
    {
        get => GetPropertyValue<IPaperItem?>();
        set => SetPropertyValue(value);
    }

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        SelectedPaperItem = null;
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        this.paperEditor.PaperItemSelected += PaperEditor_PaperItemSelected;
    }

    private void PaperEditor_PaperItemSelected(IPaperEditor sender, PlatformEventArgs<IPaperItem> e)
    {
        if (e.Data?.IsSelected == true)
        {
            SelectedPaperItem = e.Data;
            return;
        }

        SelectedPaperItem = null;
    }
}
