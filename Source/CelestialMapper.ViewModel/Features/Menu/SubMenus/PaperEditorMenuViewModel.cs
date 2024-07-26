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

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }
}
