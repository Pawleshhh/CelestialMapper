namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ExportMenuViewModelTest : ViewModelTest<ExportMenuViewModel>
{
    public override Func<ExportMenuViewModel> CreateSUT => () => new ExportMenuViewModel(ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("ExportMenu");
}
