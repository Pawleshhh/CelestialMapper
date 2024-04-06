namespace CelestialMapper.UI;

public delegate void PropertyChangedCallback<TDp, TPropertyType>(TDp d, DependencyPropertyChangedEventArgs<TPropertyType> e);
