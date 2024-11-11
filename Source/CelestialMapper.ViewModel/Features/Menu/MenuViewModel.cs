using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

[Export(typeof(MenuViewModel), IsSingleton = false, Key = nameof(MenuViewModel))]
public class MenuViewModel : ViewModelBase
{

    private readonly IIoCManager ioCManager;

    public MenuViewModel(IIoCManager iocManager, IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
        this.ioCManager = iocManager;
    }

    #region ViewModelBase

    public override FeatureName DefaultFeatureName => FeatureNames.PropertiesMenu;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        SubMenus.AddRange(configurator.GetSubViewModels().Cast<IMenuItemViewModel>());
    }

    public override Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators()
    {
        return new()
        {
            [DefaultFeatureName] = IViewModelConfigurator.Create(DefaultFeatureName, GetPropertiesSubMenuViewModels),
            [FeatureNames.ToolboxMenu] = IViewModelConfigurator.Create(FeatureNames.ToolboxMenu, GetToolboxSubMenuViewModels)
        };

        IEnumerable<IViewModel> GetPropertiesSubMenuViewModels()
        {
            yield return this.ioCManager.ServiceProvider.ResolveViewModel<PaperEditorMenuViewModel>(FeatureNames.PaperEditorMenu);
            yield return this.ioCManager.ServiceProvider.ResolveViewModel<ExportMenuViewModel>(FeatureNames.ExportMenu);
        }

        IEnumerable<IViewModel> GetToolboxSubMenuViewModels()
        {
            yield return this.ioCManager.ServiceProvider.ResolveViewModel<PaperItemsCollectionViewModel>(FeatureNames.PaperItemsCollection);
        }
    }

    #endregion

    #region Properties

    public ObservableCollection<IMenuItemViewModel> SubMenus { get; } = new();

    #endregion

}
