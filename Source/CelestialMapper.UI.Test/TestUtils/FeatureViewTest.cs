using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

public abstract class FeatureViewTest<TFeatureView, TViewModel>
    where TFeatureView : FeatureViewBase
    where TViewModel : IViewModel
{

    internal UiTestHelper UiTestHelper { get; } = new();

    #region Mocks

    public Mock<IServiceProvider> ServiceProvider { get; } = new(MockBehavior.Strict);

    #endregion

    #region FeatureViewBase

    public Type ViewModelType => typeof(TViewModel);
    public abstract string DefaultFeatureName { get; }

    #endregion

    #region SetUp

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        ServiceProvider
            .Setup(x => x.GetService(ViewModelType))
            .Returns(Mock.Of<IViewModel>());
    }

    #endregion

    #region Sut

    public abstract Func<IServiceProvider, TFeatureView> FeatureViewFactory { get; }

    public TFeatureView CreateFeatureView()
    {
        return FeatureViewFactory(ServiceProvider.Object);
    }

    public TFeatureView CreateInitializedFeatureView()
    {
        var fv = CreateFeatureView();
        fv.InitializeViewModel();
        return fv;
    }

    #endregion

    #region Test

    [Test]
    public void DefaultFeatureName_AsExpected()
    {
        UiTestHelper.RunTest(() =>
        {
            // Arrange & Act
            var featureView = CreateFeatureView();

            // Assert
            Assert.That(featureView.DefaultFeatureName, Is.EqualTo(DefaultFeatureName));
        });
    }

    [Test]
    public void FeatureName_SetProperty_Success()
    {
        UiTestHelper.RunTest(() =>
        {
            // Arrange
            var featureView = CreateFeatureView();
            const string expectedFeatureName = "TestFeature";

            // Act
            featureView.FeatureName = expectedFeatureName;

            // Assert
            Assert.That(featureView.FeatureName, Is.EqualTo(expectedFeatureName));
        });
    }

    [Test]
    public void InitializeViewModel_DataContextSet()
    {
        UiTestHelper.RunTest(() =>
        {
            // Arrange
            var vmMock = Mock.Of<IViewModel>();
            ServiceProvider
                .Setup(x => x.GetService(ViewModelType))
                .Returns(vmMock);

            var featureView = CreateFeatureView();

            // Act
            featureView.InitializeViewModel();

            // Assert
            Assert.That(featureView.DataContext, Is.SameAs(vmMock));
        });
    }

    #endregion

}
