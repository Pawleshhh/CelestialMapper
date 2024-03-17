using CelestialMapper.TestUtilities;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ShellViewModelTest : TestBase<ShellViewModel>
{

    #region SetUp

    public Mock<IViewModelSupport> ViewModelSupport { get; private set; } = new();

    public override Func<ShellViewModel> CreateSUT => () => new ShellViewModel(ViewModelSupport.Object);

    [SetUp]
    public void SetUp()
    {
        ViewModelSupport = new Mock<IViewModelSupport>(MockBehavior.Strict);
        ViewModelSupport.SetupGet(x => x.ResourceResolver).Returns(new Mock<IResourceResolver>().Object);
    }

    #endregion

    #region Tests

    [Test]
    public void Initialize_Passes()
    {
        CreateSUT().Initialize(new GenericViewModelConfigurator()
        {
            GetFeatureNameFunc = () => "Shell",
            GetSubViewModelsFunc = () => Enumerable.Empty<IViewModel>()
        });
    }

    #endregion

}
