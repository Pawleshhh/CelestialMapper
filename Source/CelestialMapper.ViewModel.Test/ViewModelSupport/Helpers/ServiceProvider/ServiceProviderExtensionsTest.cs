using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ServiceProviderExtensionsTest
{

    #region Mocks

    private static FeatureName ViewModelName = new("ViewModelName");

    #endregion

    [Test]
    public void ResolveViewModel_WithTypeAndFeatureName_ReturnsInitializedViewModel()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var viewModelMock = new Mock<IViewModel>();

        viewModelMock
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<FeatureName>()))
            .Returns(IViewModelConfigurator.Create(ViewModelName));
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModel)))
            .Returns(viewModelMock.Object);

        // Act
        var resolvedViewModel = ServiceProviderExtensions
            .ResolveViewModel(serviceProviderMock.Object, typeof(IViewModel), ViewModelName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resolvedViewModel, Is.SameAs(viewModelMock.Object));
            viewModelMock.Verify(vm => vm.Initialize(
                                    It.Is<IViewModelConfigurator>(vmc => vmc.GetFeatureName() == ViewModelName)), 
                                    Times.Once);
        });
    }

    [Test]
    public void ResolveViewModel_WithTypeAndFeatureName_ThrowsExceptionWhenViewModelIsNull()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(sp => sp.GetService(typeof(IViewModel))).Returns(null!);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            ServiceProviderExtensions.ResolveViewModel(serviceProviderMock.Object, typeof(IViewModel), ViewModelName));
    }

    [Test]
    public void ResolveViewModel_WithGenericAndFeatureName_ReturnsInitializedViewModel()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var viewModelMock = new Mock<IViewModel>();

        viewModelMock
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<FeatureName>()))
            .Returns(IViewModelConfigurator.Create(ViewModelName));
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModel)))
            .Returns(viewModelMock.Object);

        // Act
        var resolvedViewModel = ServiceProviderExtensions
            .ResolveViewModel<IViewModel>(serviceProviderMock.Object, ViewModelName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resolvedViewModel, Is.SameAs(viewModelMock.Object));
            viewModelMock.Verify(vm => vm.Initialize(
                                    It.Is<IViewModelConfigurator>(vmc => vmc.GetFeatureName() == ViewModelName)),
                                    Times.Once);
        });
    }

    [Test]
    public void ResolveViewModel_WithGenericAndFeatureName_ThrowsExceptionWhenViewModelIsNull()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(sp => sp.GetService(typeof(IViewModel))).Returns(null!);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            ServiceProviderExtensions.ResolveViewModel<IViewModel>(serviceProviderMock.Object, ViewModelName));
    }

    [Test]
    public void ResolveViewModel_Type_And_WithPostConfigure_InvokesPostConfigure()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var viewModelMock = new Mock<IViewModel>();

        viewModelMock
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<FeatureName>()))
            .Returns(IViewModelConfigurator.Create(ViewModelName));
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModel)))
            .Returns(viewModelMock.Object);
        IViewModel? postConfigVm = null;
        Action<IViewModel> postConfig = vm => postConfigVm = vm;

        // Act
        var resolvedViewModel = ServiceProviderExtensions
            .ResolveViewModel(serviceProviderMock.Object, typeof(IViewModel), ViewModelName, postConfig);

        // Assert
        Assert.That(postConfigVm, Is.Not.Null);
    }

    [Test]
    public void ResolveViewModel_Generic_And_WithPostConfigure_InvokesPostConfigure()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var viewModelMock = new Mock<IViewModel>();

        viewModelMock
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<FeatureName>()))
            .Returns(IViewModelConfigurator.Create(ViewModelName));
        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModel)))
            .Returns(viewModelMock.Object);
        IViewModel? postConfigVm = null;
        Action<IViewModel> postConfig = vm => postConfigVm = vm;

        // Act
        var resolvedViewModel = ServiceProviderExtensions
            .ResolveViewModel(serviceProviderMock.Object, ViewModelName, postConfig);

        // Assert
        Assert.That(postConfigVm, Is.Not.Null);
    }
}