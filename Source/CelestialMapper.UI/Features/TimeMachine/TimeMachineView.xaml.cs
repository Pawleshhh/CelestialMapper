
namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for TimeMachineView.xaml
/// </summary>
public partial class TimeMachineView : FeatureViewBase
{
    public TimeMachineView()
    {
        InitializeComponent();
    }

    public override string DefaultFeatureName => FeatureNames.TimeMachine;

    protected override Type ViewModelType => typeof(TimeMachineViewModel);
}
