using CelestialMapper.Common;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class MenuViewModelTest : ViewModelTest<MenuViewModel>
{
    #region Mocks

    public Mock<IIoCManager> IocManager { get; set; } = new();

    public Mock<IServiceProvider> ServiceProvider { get; set; } = new();

    public Mock<IPaperEditor> PaperEditor { get; set; } = new();

    #endregion

    public override Func<MenuViewModel> CreateSUT => () => new MenuViewModel(IocManager.Object, ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("PropertiesMenu");

    [SetUp]
    public void SetUp()
    {
        ServiceProvider = new(MockBehavior.Strict);

        ServiceProvider.Setup(x => x.GetService(typeof(PaperEditorMenuViewModel)))
            .Returns(new PaperEditorMenuViewModel(PaperEditor.Object, ViewModelSupport.Object));
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
                .Setup(x => x.TryResolveString($"String.FeatureName.PaperEditorMenu", out stringResource))
                .Returns(true);
        ResourceResolver
                .Setup(x => x.TryResolveString($"String.FeatureName.ExportMenu", out stringResource))
                .Returns(true);
        var sut = CreateSUTAndInitialize();

        var subMenus = sut.SubMenus;

        Assert.Multiple(() =>
        {
            Assert.That(subMenus, Is.Not.Empty);
            Assert.That(subMenus[0], Is.TypeOf<PaperEditorMenuViewModel>());
            Assert.That(subMenus[1], Is.TypeOf<ExportMenuViewModel>());
        });
    }
}
