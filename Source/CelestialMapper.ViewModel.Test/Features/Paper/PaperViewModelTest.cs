namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperViewModelTest : ViewModelTest<PaperViewModel>
{
    public override Func<PaperViewModel> CreateSUT => () => new PaperViewModel(ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("Paper");

}
