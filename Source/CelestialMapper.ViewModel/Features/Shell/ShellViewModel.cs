namespace CelestialMapper.ViewModel;

[Export(typeof(ShellViewModel), IsSingleton = true, Key = nameof(ShellViewModel))]
public class ShellViewModel : ViewModelBase
{

    #region Constructors

    public ShellViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    #endregion

    #region Base

    public override string DefaultFeatureName => FeatureNames.Shell;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }

    #endregion

}
