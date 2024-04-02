namespace CelestialMapper.UI;

public abstract class InputModeStrategy : DependencyObject
{

    public abstract InputMode Mode { get; }

    public int MaxLength
    {
        get { return (int)GetValue(MaxLengthProperty); }
        set { SetValue(MaxLengthProperty, value); }
    }

    public static readonly DependencyProperty MaxLengthProperty =
        DependencyProperty.Register(nameof(MaxLength), typeof(int), typeof(InputModeStrategy), new PropertyMetadata(int.MaxValue));

    public virtual bool IsCorrect(string text)
    {
        if (!IsLengthCorrect(text.Length))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsLengthCorrect(int length)
        => length <= MaxLength;

}
