namespace CelestialMapper.ViewModel;

using System.Collections.ObjectModel;

[Export(typeof(PaperViewModel), IsSingleton = false, Key = nameof(PaperViewModel))]
public class PaperViewModel : ViewModelBase
{

    #region Constructor

    public PaperViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
    }

    #endregion

    #region ViewModelBase

    public override FeatureName DefaultFeatureName => FeatureNames.Paper;

    public override void Initialize(IViewModelConfigurator configurator)
    {
        base.Initialize(configurator);

        PaperItems = new();
        PaperItems.AddRange(configurator.GetSubViewModels());
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

    #endregion

    #region Properties

    public ObservableCollection<IViewModel> PaperItems
    {
        get => GetPropertyValue<ObservableCollection<IViewModel>>()!;
        private set => SetPropertyValue(value);
    }

    #endregion
}
