using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

public abstract class ValidationRuleTestBase
{
    #region SetUp

    public Mock<IServiceProvider> ServiceProvider { get; set; } = new();

    public Mock<IResourceResolver> ResourceResolver { get; set; } = new(MockBehavior.Strict);

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ServiceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
        ServiceProvider.Setup(x => x.GetService(typeof(IResourceResolver)))
            .Returns(ResourceResolver.Object);

        string result = string.Empty;
        ResourceResolver
            .Setup(x => x.TryResolveString(It.IsAny<string>(), out result))
            .Returns(false);
    }

    #endregion

}
