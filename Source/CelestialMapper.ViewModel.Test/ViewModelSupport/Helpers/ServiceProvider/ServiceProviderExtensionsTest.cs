﻿using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ServiceProviderExtensionsTest
{

    #region Mocks

    private const string ViewModelName = "ViewModelName";

    #endregion

    [Test]
    public void ResolveViewModel_WithTypeAndFeatureName_ReturnsInitializedViewModel()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var viewModelMock = new Mock<IViewModel>();

        viewModelMock
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<string>()))
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
            .Setup(vm => vm.GetViewModelConfigurator(It.IsAny<string>()))
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
}