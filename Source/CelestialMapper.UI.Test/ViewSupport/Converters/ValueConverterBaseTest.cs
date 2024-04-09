﻿using System.Globalization;
using System.Windows.Data;

namespace CelestialMapper.UI;

public class ValueConverterBaseTest
{

    [Test]
    public void Convert_ValueIsNotExpectedType_HandlesByDoingNothing()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, object>(OnWrongType.DoNothing);

        // Act
        var result = valueConverter.Convert("notInt", typeof(int), null, null);

        // Assert
        Assert.That(result, Is.SameAs(Binding.DoNothing));
    }

    [Test]
    public void Convert_ValueIsNotExpectedType_HandlesByReturningDefault()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, object>(OnWrongType.ReturnDefault, defaultToValue: "default");

        // Act
        var result = valueConverter.Convert("notInt", typeof(int), null, null);

        // Assert
        Assert.That(result, Is.EqualTo("default"));
    }

    [Test]
    public void Convert_ValueIsNotExpectedType_HandlesByThrowingException()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, object>(OnWrongType.ThrowException);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => valueConverter.Convert("notInt", typeof(int), null, null));
    }

    [Test]
    public void Convert_ParameterIsNotExpectedType_OnWrongTypeIsDoingNothing_HandlesByDoingNothing()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, string>(OnWrongType.DoNothing, true);

        // Act
        var result = valueConverter.Convert(3, typeof(int), 64m, null);

        // Assert
        Assert.That(result, Is.SameAs(Binding.DoNothing));
    }

    [Test]
    public void Convert_ParameterIsNotExpectedType_OnWrongTypeIsReturnsDefault_HandlesByThrowingException()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, string>(OnWrongType.ReturnDefault, true);

        // Act
        Assert.Throws<InvalidOperationException>(() => valueConverter.Convert(3, typeof(int), 64m, null));
    }

    [Test]
    public void Convert_ParameterIsNotExpectedType_OnWrongTypeIsThrowException_HandlesByThrowingException()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<int, string, string>(OnWrongType.ThrowException, true);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => valueConverter.Convert("notInt", typeof(int), 64m, null));
    }

    [Test]
    public void Convert_ConvertsFromDataToOtherData_ReturnsExpectedValue()
    {
        // Arrange
        IValueConverter valueConverter = new ValueConverterBaseMock<string, int, double>(expectsParameter: true)
        {
            ConvertFunc = (f, p) =>
            {
                if (f == "Text" && p == 4.3)
                {
                    return 10;
                }

                return -100;
            }
        };

        // Act
        var result = valueConverter.Convert("Text", typeof(string), 4.3, null);

        // Assert
        Assert.That(result, Is.EqualTo(10));
    }

}

public class ValueConverterBaseMock<TFrom, TTo, TParameter> : ValueConverterBase<TFrom, TTo, TParameter>
    where TFrom : notnull
    where TTo : notnull
{

    public override OnWrongType OnWrongType { get; }

    public override bool ExpectsParameter { get; }

    public override TFrom? DefaultFromValue { get; }

    public override TTo? DefaultToValue { get; }

    public Func<TFrom, TParameter, TTo?> ConvertFunc { get; set; } 
    public Func<TTo, TParameter, TFrom?> ConvertBackFunc { get; set; }

    public ValueConverterBaseMock(
        OnWrongType onWrongType = OnWrongType.DoNothing,
        bool expectsParameter = false,
        TFrom? defaultFromValue = default,
        TTo? defaultToValue = default)
    {
        OnWrongType = onWrongType;
        ExpectsParameter = expectsParameter;
        DefaultFromValue = defaultFromValue;
        DefaultToValue = defaultToValue;

        ConvertFunc = (f, p) => DefaultToValue;
        ConvertBackFunc = (t, p) => DefaultFromValue;
    }

    public override TTo Convert(TFrom value, Type targetType, TParameter parameter, CultureInfo culture)
    {
        return ConvertFunc(value, parameter)!;
    }

    public override TFrom ConvertBack(TTo value, Type targetType, TParameter parameter, CultureInfo culture)
    {
        return ConvertBackFunc(value, parameter)!;
    }

}