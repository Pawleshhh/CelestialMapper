namespace CelestialMapper.UI;

public struct DependencyPropertyChangedEventArgs<TPropertyType>
{

    private readonly DependencyPropertyChangedEventArgs eventArgs;

    public TPropertyType NewValue => (TPropertyType)this.eventArgs.NewValue;

    public TPropertyType OldValue => (TPropertyType)this.eventArgs.OldValue;

    public DependencyProperty Property => this.eventArgs.Property;

    public DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs eventArgs)
    {
        this.eventArgs = eventArgs;
    }

}
