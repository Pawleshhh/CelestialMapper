namespace CelestialMapper.UI;


public delegate TProperty CoerceValueCallback<TDepObj, TProperty>(TDepObj d, TProperty baseValue)
    where TDepObj : DependencyObject;
