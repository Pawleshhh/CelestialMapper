namespace CelestialMapper.ViewModel;

public interface IViewModelConfigurator
{

    IEnumerable<IViewModel> GetSubViewModels();

    FeatureName GetFeatureName();

    public static IViewModelConfigurator Create(FeatureName featureName)
        => new GenericViewModelConfigurator
        { 
            GetFeatureNameFunc = () => featureName,
            GetSubViewModelsFunc = Enumerable.Empty<IViewModel>
        };

    public static IViewModelConfigurator Create(FeatureName featureName, Func<IEnumerable<IViewModel>> getSubViewModels)
        => new GenericViewModelConfigurator
        {
            GetFeatureNameFunc = () => featureName,
            GetSubViewModelsFunc = getSubViewModels
        };

}

public class GenericViewModelConfigurator : IViewModelConfigurator
{

    public required Func<FeatureName> GetFeatureNameFunc { get; init; }

    public required Func<IEnumerable<IViewModel>> GetSubViewModelsFunc { get; init; }

    public FeatureName GetFeatureName() => GetFeatureNameFunc();

    public IEnumerable<IViewModel> GetSubViewModels() => GetSubViewModelsFunc();

}