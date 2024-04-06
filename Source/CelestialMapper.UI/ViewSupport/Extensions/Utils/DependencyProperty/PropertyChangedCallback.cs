namespace CelestialMapper.UI;

public delegate void PropertyChangedCallback<TDepObj, TPropertyType>(TDepObj d, DependencyPropertyChangedEventArgs<TPropertyType> e)
    where TDepObj : DependencyObject;
