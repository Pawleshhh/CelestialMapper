namespace CelestialMapper.UI;

using static CelestialMapper.UI.DependencyPropertyHelper;

public class CommandZIndexParameters : DependencyObject, ICommandZIndexParameters
{
    public object[] Parameters { get; set; } = new object[2];

    public object Sender
    {
        get => GetValue(SenderProperty);
        set
        {
            Parameters[0] = value;
            SetValue(SenderProperty, value);
        }
    }

    public static readonly DependencyProperty SenderProperty =
        Register(nameof(Sender), new PlatformPropertyMetadata<CommandZIndexParameters, object>());

    public ZIndexAction ZIndexAction
    {
        get => this.GetValue<ZIndexAction>(ZIndexActionProperty);
        set
        {
            Parameters[1] = value;
            SetValue(ZIndexActionProperty, value);
        }
    }

    public static readonly DependencyProperty ZIndexActionProperty =
        Register(nameof(ZIndexAction), new PlatformPropertyMetadata<CommandZIndexParameters, ZIndexAction>());
}
