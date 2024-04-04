using static CelestialMapper.UI.DependencyPropertyHelper;

namespace CelestialMapper.UI;

public class LabeledTextBox : TextBox
{

    public static readonly string LabeledTextBoxDefaultStyleKey = "Style.LabeledTextBox";

    public LabeledTextBox()
    {
        Style = TryFindResource(LabeledTextBoxDefaultStyleKey) as Style;
    }

    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledTextBox), new(string.Empty));

    public TextPlacement LabelPlacement
    {
        get { return (TextPlacement)GetValue(LabelPlacementProperty); }
        set { SetValue(LabelPlacementProperty, value); }
    }

    public static readonly DependencyProperty LabelPlacementProperty =
        DependencyProperty.Register(nameof(LabelPlacement), typeof(TextPlacement), typeof(LabeledTextBox), new(TextPlacement.Left));

    public Style LabelStyle
    {
        get { return (Style)GetValue(LabelStyleProperty); }
        set { SetValue(LabelStyleProperty, value); }
    }

    public static readonly DependencyProperty LabelStyleProperty =
        DependencyProperty.Register(nameof(LabelStyle), typeof(Style), typeof(LabeledTextBox), new(null));

    #region InputMode

    //public InputModeStrategy InputModeStrategy
    //{
    //    get { return (InputModeStrategy)GetValue(InputModeStrategyProperty); }
    //    set { SetValue(InputModeStrategyProperty, value); }
    //}

    //public static readonly DependencyProperty InputModeStrategyProperty =
    //    DependencyProperty.Register(nameof(InputModeStrategy), typeof(InputModeStrategy), typeof(LabeledTextBox), new(null, OnInputModeStrategyChanged));

    //private static void OnInputModeStrategyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    if (!CanHandle<LabeledTextBox, InputModeStrategy>(d, e, out var labeledTextBox, out var inputModeStrategy))
    //    {
    //        return;
    //    }

    //    labeledTextBox.OnInputModeStrategyChanged(inputModeStrategy);
    //}

    //protected virtual void OnInputModeStrategyChanged(InputModeStrategy inputModeStrategy)
    //{
    //    var inputMode = inputModeStrategy.Mode;

    //    if (InputMode != inputMode)
    //    {
    //        InputMode = inputMode;
    //    }
    //}

    //public InputMode InputMode
    //{
    //    get { return (InputMode)GetValue(InputModeProperty); }
    //    set { SetValue(InputModeProperty, value); }
    //}

    //public static readonly DependencyProperty InputModeProperty =
    //    DependencyProperty.Register(nameof(InputMode), typeof(InputMode), typeof(LabeledTextBox), new(InputMode.String, OnInputModeChanged));

    //private static void OnInputModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    if (!CanHandle<LabeledTextBox, InputMode>(d, e, out var labeledTextBox, out var inputMode))
    //    {
    //        return;
    //    }

    //    labeledTextBox.OnInputModeChanged(inputMode);
    //}

    //protected virtual void OnInputModeChanged(InputMode inputMode)
    //{
    //    if (InputMode == InputMode.String &&
    //        InputModeStrategy is not TextInputModeStrategy)
    //    {
    //        InputModeStrategy = new TextInputModeStrategy();
    //    }

    //    if (InputMode == InputMode.Double &&
    //        InputModeStrategy is not DoubleInputModeStrategy)
    //    {
    //        InputModeStrategy = new DoubleInputModeStrategy();
    //    }
    //}

    #endregion

}
