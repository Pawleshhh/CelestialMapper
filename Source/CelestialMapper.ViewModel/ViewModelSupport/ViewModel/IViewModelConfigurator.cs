namespace CelestialMapper.ViewModel;

public interface IViewModelConfigurator
{

    IEnumerable<IViewModel> GetSubViewModels();

    string GetFeatureName();

    public static IViewModelConfigurator Create(string featureName)
        => new GenericViewModelConfigurator
        { 
            GetFeatureNameFunc = () => featureName,
            GetSubViewModelsFunc = Enumerable.Empty<IViewModel>
        };

    public static IViewModelConfigurator Create(string featureName, Func<IEnumerable<IViewModel>> getSubViewModels)
        => new GenericViewModelConfigurator
        {
            GetFeatureNameFunc = () => featureName,
            GetSubViewModelsFunc = getSubViewModels
        };

}

public class GenericViewModelConfigurator : IViewModelConfigurator
{

    public required Func<string> GetFeatureNameFunc { get; init; }

    public required Func<IEnumerable<IViewModel>> GetSubViewModelsFunc { get; init; }

    public string GetFeatureName() => GetFeatureNameFunc();

    public IEnumerable<IViewModel> GetSubViewModels() => GetSubViewModelsFunc();

}