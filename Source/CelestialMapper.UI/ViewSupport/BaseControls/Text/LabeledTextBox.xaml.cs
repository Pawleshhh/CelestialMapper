namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for LabeledTextBox.xaml
/// </summary>
public partial class LabeledTextBox : TextBox
{

    public LabeledTextBox()
    {
        InitializeComponent();
    }

    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledTextBox), new PropertyMetadata(string.Empty));

    public TextPlacement LabelPlacement
    {
        get { return (TextPlacement)GetValue(LabelPlacementProperty); }
        set { SetValue(LabelPlacementProperty, value); }
    }

    public static readonly DependencyProperty LabelPlacementProperty =
        DependencyProperty.Register(nameof(LabelPlacement), typeof(TextPlacement), typeof(LabeledTextBox), new PropertyMetadata(TextPlacement.Left));

}
