namespace CelestialMapper.UI;

public class LabeledTextBox : TextBox
{

    public static readonly string LabeledTextBoxDefaultStyleKey = "Style.LabeledText";

    public LabeledTextBox()
    {
        Style = FindResource(LabeledTextBoxDefaultStyleKey) as Style;
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

    public Style LabelStyle
    {
        get { return (Style)GetValue(LabelStyleProperty); }
        set { SetValue(LabelStyleProperty, value); }
    }

    public static readonly DependencyProperty LabelStyleProperty =
        DependencyProperty.Register(nameof(LabelStyle), typeof(Style), typeof(LabeledTextBox), new PropertyMetadata(null));

}
