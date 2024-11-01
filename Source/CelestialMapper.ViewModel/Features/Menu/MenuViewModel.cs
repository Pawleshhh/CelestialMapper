﻿using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

[Export(typeof(MenuViewModel), IsSingleton = true, Key = nameof(MenuViewModel))]
public class MenuViewModel : ViewModelBase
{

    private readonly IIoCManager ioCManager;

    public MenuViewModel(IIoCManager iocManager, IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
        this.ioCManager = iocManager;
    }

    #region ViewModelBase

    public override FeatureName DefaultFeatureName => FeatureNames.Menu;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        SubMenus.AddRange(configurator.GetSubViewModels());
    }

    public override Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators()
    {
        return new()
        {
            [DefaultFeatureName] = IViewModelConfigurator.Create(DefaultFeatureName, GetSubMenuViewModels)
        };

        IEnumerable<IViewModel> GetSubMenuViewModels()
        {
            yield return this.ioCManager.ServiceProvider.ResolveViewModel<PaperEditorMenuViewModel>(FeatureNames.PaperEditorMenu);
            yield return this.ioCManager.ServiceProvider.ResolveViewModel<ExportMenuViewModel>(FeatureNames.ExportMenu);
        }
    }

    #endregion

    #region Properties

    public ObservableCollection<IViewModel> SubMenus { get; } = new();

    #endregion

}
