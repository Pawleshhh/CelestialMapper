namespace CelestialMapper.ViewModel;

[Export(typeof(MenuViewModel), IsSingleton = true, Key = nameof(MenuViewModel))]
public class MenuViewModel : ViewModelBase
{
    public MenuViewModel(IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
    }

    #region ViewModelBase

    public override string DefaultFeatureName => FeatureNames.Menu;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);
    }

    #endregion

}
