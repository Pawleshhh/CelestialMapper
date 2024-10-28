using System.Globalization;
using System.Windows.Data;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class ConverterGroupTest
{

    [Test]
    public void Convert_GoThroughConverters_ReturnFinalValue()
    {
        // Arrange
        var converterGroup = new ConverterGroup<int, string, object>(
            new ValueConverter1(),
            new ValueConverter2(),
            new ValueConverter3());

        // Act
        var result = converterGroup.Convert(10, null!, null, null!);

        // Assert
        Assert.That(result, Is.EqualTo("22"));
    }

    [Test]
    public void ConvertBack_GoThroughConverters_ReturnFinalValue()
    {
        // Arrange
        var converterGroup = new ConverterGroup<int, string, object>(
            new ValueConverter1(),
            new ValueConverter2(),
            new ValueConverter3());

        // Act
        var result = converterGroup.ConvertBack("22", null!, null, null!);

        // Assert
        Assert.That(result, Is.EqualTo(10));
    }

    private class ValueConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value + 1;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value - 1;
    }

    private class ValueConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value * 2;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (int)value / 2;
    }

    private class ValueConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value.ToString()!;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => int.Parse(value.ToString()!);
    }
}
