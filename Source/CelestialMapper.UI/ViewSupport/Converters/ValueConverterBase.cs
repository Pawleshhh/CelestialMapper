﻿using System.Globalization;
using System.Windows.Data;

namespace CelestialMapper.UI;

public abstract class ValueConverterBase<TFrom, TTo, TParameter> : IValueConverter
    where TFrom : notnull
    where TTo : notnull
{

    public virtual OnWrongType OnWrongType { get; } = OnWrongType.DoNothing;

    public virtual bool ExpectsParameter { get; } = false;

    public virtual TFrom? DefaultFromValue { get; } = default;

    public virtual TTo? DefaultToValue { get; } = default;

    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TFrom from)
        {
            return HandleWrongFromType(value)!;
        }
        if (ExpectsParameter && parameter is not TParameter)
        {
            return HandleWrongParameterType(parameter);
        }

        var param = (TParameter)parameter;

        return Convert(from, targetType, param, culture);
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TTo to)
        {
            return HandleWrongToType(value)!;
        }
        if (ExpectsParameter && parameter is not TParameter)
        {
            return HandleWrongParameterType(parameter);
        }

        var param = (TParameter)parameter;

        return ConvertBack(to, targetType, param, culture);
    }

    public abstract TTo Convert(TFrom value, Type targetType, TParameter parameter, CultureInfo culture);

    public abstract TFrom ConvertBack(TTo value, Type targetType, TParameter parameter, CultureInfo culture);

    private object? HandleWrongFromType(object obj)
    {
        if (OnWrongType == OnWrongType.DoNothing)
        {
            return Binding.DoNothing;
        }
        if (OnWrongType == OnWrongType.ReturnDefault)
        {
            return DefaultToValue;
        }

        throw new InvalidOperationException($"Expected type {typeof(TFrom)} but instead got {obj.GetType()}");
    }

    private object? HandleWrongToType(object obj)
    {
        if (OnWrongType == OnWrongType.DoNothing)
        {
            return Binding.DoNothing;
        }
        if (OnWrongType == OnWrongType.ReturnDefault)
        {
            return DefaultFromValue;
        }

        throw new InvalidOperationException($"Expected type {typeof(TTo)} but instead got {obj.GetType()}");
    }

    private object HandleWrongParameterType(object obj)
    {
        if (OnWrongType == OnWrongType.DoNothing)
        {
            return Binding.DoNothing;
        }

        throw new InvalidOperationException($"Expected parameter of type {typeof(TParameter)} but instead got {obj.GetType()}");
    }
}

public enum OnWrongType
{
    DoNothing,
    ThrowException,
    ReturnDefault,
}