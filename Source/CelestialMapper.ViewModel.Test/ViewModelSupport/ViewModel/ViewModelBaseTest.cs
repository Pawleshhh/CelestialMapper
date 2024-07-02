using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ViewModelBaseTest
{
    private class TestViewModel : ViewModelBase
    {
        public TestViewModel(IViewModelSupport viewModelSupport) : base(viewModelSupport)
        {
        }

        public override FeatureName DefaultFeatureName => new("DefaultFeature");
    }

    [Test]
    public void Initialize_SetsFeatureNameAndName()
    {
        string featureName = "FeatureName";

        var viewModelSupportMock = new Mock<IViewModelSupport>();
        viewModelSupportMock
            .Setup(x => x.ResourceResolver.TryResolveString(It.IsAny<string>(), out featureName))
            .Returns(true);

        var configuratorMock = new Mock<IViewModelConfigurator>();
        configuratorMock.Setup(configurator => configurator.GetFeatureName()).Returns(new FeatureName("TestFeature"));

        var viewModel = new TestViewModel(viewModelSupportMock.Object);
        viewModel.Initialize(configuratorMock.Object);
        
        Assert.Multiple(() =>
        {
            Assert.That(viewModel.FeatureName, Is.EqualTo(new FeatureName("TestFeature")));
            Assert.That(viewModel.Name, Is.EqualTo(featureName));
        });
    }

    [Test]
    public void Unitilialize_DoesNotThrow()
    {
        var viewModelSupportMock = new Mock<IViewModelSupport>();
        var viewModel = new TestViewModel(viewModelSupportMock.Object);

        Assert.DoesNotThrow(() => viewModel.Unitilialize());
    }

    [Test]
    public void InitializeConfigurators_ReturnsDictionaryWithDefaultConfigurator()
    {
        var viewModelSupportMock = new Mock<IViewModelSupport>();
        var viewModel = new TestViewModel(viewModelSupportMock.Object);

        Dictionary<FeatureName, IViewModelConfigurator> configurators = viewModel.InitializeConfigurators();

        Assert.That(configurators, Is.Not.Null.And.Not.Empty);
        Assert.That(configurators.ContainsKey(viewModel.DefaultFeatureName), Is.True);
    }
}
