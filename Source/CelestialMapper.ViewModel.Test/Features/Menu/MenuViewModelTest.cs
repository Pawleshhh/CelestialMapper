using CelestialMapper.Common;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class MenuViewModelTest : ViewModelTest<MenuViewModel>
{
    #region Mocks

    public Mock<IIoCManager> IocManager { get; set; } = new();

    public Mock<IServiceProvider> ServiceProvider { get; set; } = new();

    #endregion

    public override Func<MenuViewModel> CreateSUT => () => new MenuViewModel(IocManager.Object, ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("Menu");

    [SetUp]
    public void SetUp()
    {
        ServiceProvider = new(MockBehavior.Strict);
        ServiceProvider.Setup(x => x.GetService(typeof(ExportMenuViewModel)))
            .Returns(new ExportMenuViewModel(ViewModelSupport.Object));

        IocManager = new(MockBehavior.Strict);
        IocManager.SetupGet(x => x.ServiceProvider).Returns(ServiceProvider.Object);
    }

    [Test]
    public void Initialize_FillSubMenus_ReturnsExpectedSubMenus()
    {
        var stringResource = "Some Name";
        ResourceResolver
                .Setup(x => x.TryResolveString($"String.FeatureName.ExportMenu", out stringResource))
                .Returns(true);
        var sut = CreateSUTAndInitialize();

        var subMenus = sut.SubMenus;

        Assert.That(subMenus[0], Is.TypeOf<ExportMenuViewModel>());
    }
}
