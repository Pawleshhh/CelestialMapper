using System.Windows;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class NullToVisibilityConverterTest
{

    [TestCase(null, Visibility.Visible, Visibility.Collapsed, Visibility.Visible)]
    [TestCase(null, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed)]
    [TestCase("Something", Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed)]
    [TestCase("Something", Visibility.Collapsed, Visibility.Visible, Visibility.Visible)]
    public void Convert_ConvertNull_ToVisibility(object? value, Visibility whenNull, Visibility whenNotNull, Visibility expected)
    {
        // Arrange
        var converter = new NullToVisibilityConverter()
        {
            WhenNull = whenNull,
            WhenNotNull = whenNotNull
        };

        // Act
        var result = converter.Convert(value, null!, null, null!);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

}
