namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ShellViewModelTest : ViewModelTest<ShellViewModel>
{

    #region SetUp

    public override Func<ShellViewModel> CreateSUT => () => new ShellViewModel(ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("Shell");

    #endregion

}
