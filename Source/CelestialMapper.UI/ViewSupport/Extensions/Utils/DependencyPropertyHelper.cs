namespace CelestialMapper.UI;

public static class DependencyPropertyHelper
{

    public static DependencyProperty Register<TProperty, TOwner>(string name)
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty Register<TProperty, TOwner>(string name, PropertyMetadata propertyMetadata)
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), propertyMetadata);

    public static DependencyProperty Register<TProperty, TOwner>(string name, PropertyMetadata propertyMetadata, ValidateValueCallback validateValueCallback)
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), propertyMetadata, validateValueCallback);

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, PropertyMetadata propertyMetadata)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), propertyMetadata);

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, PropertyMetadata propertyMetadata, ValidateValueCallback validateValueCallback)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), propertyMetadata, validateValueCallback);

    public static T GetValue<T>(this DependencyObject @this, DependencyProperty dp)
        => (T)@this.GetValue(dp);

    public static bool CanHandle<TDp, TValueType>(
        DependencyObject dp,
        DependencyPropertyChangedEventArgs e, 
        out TDp expectedDp, 
        out TValueType expectedValue)
    {
        if (dp is TDp exDp && e.NewValue is TValueType exValue)
        {
            expectedDp = exDp;
            expectedValue = exValue;
            return true;
        }

        expectedDp = default!;
        expectedValue = default!;
        return false;
    }

}
