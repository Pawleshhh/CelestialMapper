namespace CelestialMapper.UI;

public class PlatformPropertyMetadata<TProperty> : PlatformPropertyMetadata<DependencyObject, TProperty>
{

    public PlatformPropertyMetadata()
        : base()
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue)
        : base(defaultValue)
    {

    }

    public PlatformPropertyMetadata(PropertyChangedCallback<DependencyObject, TProperty> propertyChangedCallback)
        : base(propertyChangedCallback)
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue, PropertyChangedCallback<DependencyObject, TProperty> propertyChangedCallback)
        : base(defaultValue, propertyChangedCallback)
    {

    }

    public PlatformPropertyMetadata(
        TProperty? defaultValue, 
        PropertyChangedCallback<DependencyObject, TProperty> propertyChangedCallback,
        CoerceValueCallback<DependencyObject, TProperty> coerceValueCallback)
        : base(defaultValue, propertyChangedCallback, coerceValueCallback)
    {

    }
}

public class PlatformPropertyMetadata<TDepOb, TProperty> : PropertyMetadata
    where TDepOb : DependencyObject
{

    public new TProperty DefaultValue => (TProperty)base.DefaultValue;

    public PlatformPropertyMetadata()
        : base()
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue)
        : base(defaultValue)
    {

    }

    public PlatformPropertyMetadata(PropertyChangedCallback<TDepOb, TProperty> propertyChangedCallback)
        : base((s, e) => propertyChangedCallback.Invoke((TDepOb)s, new(e)))
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue, PropertyChangedCallback<TDepOb, TProperty> propertyChangedCallback)
        : base(defaultValue, (s, e) => propertyChangedCallback.Invoke((TDepOb)s, new(e)))
    {

    }

    public PlatformPropertyMetadata(
        TProperty? defaultValue, 
        PropertyChangedCallback<TDepOb, TProperty> propertyChangedCallback,
        CoerceValueCallback<TDepOb, TProperty> coerceValueCallback)
        : base(
            defaultValue, 
            (s, e) => propertyChangedCallback.Invoke((TDepOb)s, new(e)),
            (s, e) => coerceValueCallback.Invoke((TDepOb)s, (TProperty)e))
    {

    }
}