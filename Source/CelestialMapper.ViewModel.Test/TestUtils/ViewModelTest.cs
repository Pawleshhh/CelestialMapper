﻿using Moq;

namespace CelestialMapper.ViewModel.Test;

public abstract class ViewModelTest<TViewModel>
    where TViewModel : class, IViewModel
{

    public abstract Func<TViewModel> CreateSUT { get; }

    public abstract FeatureName DefaultFeatureName { get; }

    public virtual IViewModelConfigurator DefaultViewModelConfigurator 
        => IViewModelConfigurator.Create(DefaultFeatureName);

    public virtual Dictionary<FeatureName, IViewModelConfigurator> AllConfigurators
        => new()
        {
            [DefaultFeatureName] = DefaultViewModelConfigurator
        };

    #region Mocks

    public Mock<IViewModelSupport> ViewModelSupport { get; } = new(MockBehavior.Strict);

    public Mock<IResourceResolver> ResourceResolver { get; } = new(MockBehavior.Strict);

    #endregion

    #region SetUp

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        foreach (var config in AllConfigurators)
        {
            var value = config.Key.Name;
            ResourceResolver
                .Setup(x => x.TryResolveString($"String.FeatureName.{config.Key.Name}", out value))
                .Returns(true);
        }

        ViewModelSupport.SetupGet(x => x.ResourceResolver)
            .Returns(ResourceResolver.Object);
    }

    #endregion

    #region Base Tests

    [Test]
    public virtual void Initialize_AllConfigurators_Passes()
    {
        var sut = CreateSUT();

        Assert.Multiple(() =>
        {
            foreach (var config in AllConfigurators)
            {
                sut.Initialize(config.Value);
            }
        });
    }

    #endregion

    #region Helpers

    protected TViewModel CreateSUTAndInitialize()
    {
        var sut = CreateSUT();
        sut.Initialize(sut.GetViewModelConfigurator(DefaultFeatureName));
        return sut;
    }

    protected TViewModel CreateSUTAndInitialize(IViewModelConfigurator viewModelConfigurator)
    {
        var sut = CreateSUT();
        sut.Initialize(viewModelConfigurator);
        return sut;
    }

    #endregion

}
