using System.ComponentModel;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class GenericViewModelConfiguratorTest
{

    [Test]
    public void GetFeatureName_ReturnsExpectedValue()
    {
        FeatureName featureName = new("TestFeature");
        IViewModelConfigurator configurator = new GenericViewModelConfigurator
        {
            GetFeatureNameFunc = () => featureName,
            GetSubViewModelsFunc = Enumerable.Empty<IViewModel>
        };

        var result = configurator.GetFeatureName();

        Assert.That(result, Is.EqualTo(featureName));
    }

    [Test]
    public void GetSubViewModels_ReturnsEmptyList_WhenNoSubViewModels()
    {
        IViewModelConfigurator configurator = new GenericViewModelConfigurator
        {
            GetFeatureNameFunc = () => new("TestFeature"),
            GetSubViewModelsFunc = Enumerable.Empty<IViewModel>
        };

        IEnumerable<IViewModel> result = configurator.GetSubViewModels();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetSubViewModels_ReturnsSubViewModels()
    {
        var subViewModels = new List<IViewModel>
        {
            new TestViewModel(),
            new TestViewModel(),
            new TestViewModel()
        };

        IViewModelConfigurator configurator = new GenericViewModelConfigurator
        {
            GetFeatureNameFunc = () => new("TestFeature"),
            GetSubViewModelsFunc = () => subViewModels
        };

        IEnumerable<IViewModel> result = configurator.GetSubViewModels();

        Assert.That(result, Is.EquivalentTo(subViewModels));
    }

    [Test]
    public void Create_CreatesConfiguratorWithFeatureNameOnly()
    {
        FeatureName featureName = new("TestFeature");
        var configurator = IViewModelConfigurator.Create(featureName);

        var result = configurator.GetFeatureName();

        Assert.That(result, Is.EqualTo(featureName));
    }

    private class TestViewModel : IViewModel
    {
        public FeatureName FeatureName { get; set; } = FeatureName.Unknown;
        public string Name { get; set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Initialize(IViewModelConfigurator configurator)
        {
            
        }

        public void Unitilialize()
        {

        }

        public Dictionary<FeatureName, IViewModelConfigurator> InitializeConfigurators()
        {
            return new Dictionary<FeatureName, IViewModelConfigurator>();
        }
    }

}
