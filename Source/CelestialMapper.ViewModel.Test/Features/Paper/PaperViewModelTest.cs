using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperViewModelTest : ViewModelTest<PaperViewModel>
{

    public Mock<IPaperEditor> PaperEditor { get; } = new(MockBehavior.Strict);

    public override Func<PaperViewModel> CreateSUT => () => new PaperViewModel(
        PaperEditor.Object,
        ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => new("Paper");

}
