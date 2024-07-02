namespace CelestialMapper.Common.Test;

using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

public interface IMyService { }

[Export(typeof(IMyService), typeof(SingletonService), IsSingleton = true, Key = nameof(SingletonService))]
public class SingletonService : IMyService { }

[Export(typeof(IMyService), typeof(NonSingletonService), IsSingleton = false, Key = nameof(NonSingletonService))]
public class NonSingletonService : IMyService { }

[Export(typeof(IMyService), typeof(KeyedSingletonService), IsSingleton = true, IsKeyed = true, Key = nameof(KeyedSingletonService))]
public class KeyedSingletonService : IMyService { }

[Export(typeof(IMyService), typeof(KeyedNonSingletonService), IsSingleton = false, IsKeyed = true, Key = nameof(KeyedNonSingletonService))]
public class KeyedNonSingletonService : IMyService { }

[TestFixture]
public class IoCManagerTests
{

    [Test]
    public void Constructor_WithNullServices_ThrowsArgumentNullException()
    {
        // Arrange
        IServiceCollection? services = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new IoCManager(services!));
    }

    [Test]
    public void RegisterExports_NullAssembly_ThrowsArgumentNullException()
    {
        // Arrange
        var servicesMock = new Mock<IServiceCollection>();
        var manager = new IoCManager(servicesMock.Object);
        Assembly? assembly = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => manager.RegisterExports(assembly!));
    }

    [Test]
    public void RegisterExports_WithExportAttributes_RegistersServicesCorrectly()
    {
        // Arrange
        var servicesMock = new Mock<IServiceCollection>();
        var manager = new IoCManager(servicesMock.Object);
        var assembly = Assembly.GetExecutingAssembly();

        // Act
        manager.RegisterExports(assembly);

        // Assert
        VerifyTypeRegistration(
            servicesMock, 
            typeof(IMyService),
            typeof(SingletonService), 
            ServiceLifetime.Singleton);

        VerifyTypeRegistration(
            servicesMock,
            typeof(IMyService),
            typeof(NonSingletonService),
            ServiceLifetime.Transient);

        VerifyKeyedTypeRegistration(
            servicesMock,
            typeof(IMyService),
            typeof(KeyedSingletonService),
            nameof(KeyedSingletonService),
            ServiceLifetime.Singleton);

        VerifyKeyedTypeRegistration(
            servicesMock,
            typeof(IMyService),
            typeof(KeyedNonSingletonService),
            nameof(KeyedNonSingletonService),
            ServiceLifetime.Transient);
    }

    [Test]
    public void RegisterItself_AddsSingletonOfBothServiceCollectionAndItself()
    {
        // Arrange
        var servicesMock = new Mock<IServiceCollection>();
        var manager = new IoCManager(servicesMock.Object);

        // Act
        manager.RegisterItself();

        // Assert
        VerifyInstanceRegistration(servicesMock, typeof(IServiceCollection), servicesMock.Object);
        VerifyInstanceRegistration(servicesMock, typeof(IIoCManager), manager);
    }

    private void VerifyTypeRegistration(
        Mock<IServiceCollection> serviceCollection,
        Type serviceType,
        Type implementationType,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        => serviceCollection.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
            !sd.IsKeyedService &&
            sd.ServiceType == serviceType &&
            sd.ImplementationType == implementationType &&
            sd.Lifetime == serviceLifetime)));

    private void VerifyKeyedTypeRegistration(
       Mock<IServiceCollection> serviceCollection,
       Type serviceType,
       Type implementationType,
       string serviceKey,
       ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
       => serviceCollection.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
           sd.IsKeyedService &&
           sd.ServiceType == serviceType &&
           sd.KeyedImplementationType == implementationType &&
           sd.Lifetime == serviceLifetime &&
           sd.ServiceKey as string == serviceKey)));

    private void VerifyInstanceRegistration(
        Mock<IServiceCollection> serviceCollection,
        Type serviceType,
        object instance)
        => serviceCollection.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
            sd.ServiceType == serviceType &&
            sd.ImplementationInstance == instance)));

}