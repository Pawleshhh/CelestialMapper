using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

class MockFeatureViewBase : FeatureViewBase
{

    public MockFeatureViewBase(IServiceProvider serviceProvider)
        : base(serviceProvider, false)
    {
    }

    protected override Type ViewModelType => typeof(IViewModel);
    public override string DefaultFeatureName => "DefaultFeature";

}

[TestFixture]
public class FeatureViewBaseTest : FeatureViewTest<FeatureViewBase, IViewModel>
{

    #region Sut

    public override Func<IServiceProvider, FeatureViewBase> FeatureViewFactory 
        => sp => new MockFeatureViewBase(sp);

    public override string DefaultFeatureName => "DefaultFeature";

    #endregion

}
