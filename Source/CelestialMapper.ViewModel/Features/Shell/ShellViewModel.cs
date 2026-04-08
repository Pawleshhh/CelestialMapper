using System.Windows.Input;

namespace CelestialMapper.ViewModel;

[Export(typeof(ShellViewModel), IsSingleton = true, Key = nameof(ShellViewModel))]
public class ShellViewModel : ViewModelBase
{

    #region Fields

    private readonly OverlayToolHelper overlayToolHelper;

    #endregion

    #region Constructors

    public ShellViewModel(
        OverlayToolHelper overlayToolHelper,
        IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        this.overlayToolHelper = overlayToolHelper;
    }

    #endregion

    #region OverlayTool

    public OverlayToolData OverlayToolData { get; } = new();

    #endregion

    #region Base

    public override FeatureName DefaultFeatureName => FeatureNames.Shell;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        this.overlayToolHelper.Setup((f, d) => OverlayToolData.SetTool(true, f, d));
    }

    #endregion

}
