namespace CelestialMapper.UI.Test;

using System.Globalization;
using System.Windows;

[TestFixture]
public class PaperSizeExtensionsTest
{

    [Test]
    [TestCase(PaperSize.A3, 29.7, 42)]
    [TestCase(PaperSize.A4, 21, 29.7)]
    [TestCase(PaperSize.A5, 14.8, 21)]
    public void GetPaperSizeInCentimeters_ValidPaperSizes_ReturnsCorrectDimensions(PaperSize paperSize, double expectedWidth, double expectedHeight)
    {
        // Act
        var result = paperSize.GetPaperSizeInCentimeters();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Width, Is.EqualTo(expectedWidth));
            Assert.That(result.Height, Is.EqualTo(expectedHeight));
        });
    }

    [Test]
    public void GetPaperSizeInCentimeters_InvalidPaperSize_ThrowsNotImplementedException()
    {
        // Arrange
        PaperSize invalidSize = (PaperSize)999;

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => invalidSize.GetPaperSizeInCentimeters());
    }

    [Test]
    [TestCase(PaperSize.A3)]
    [TestCase(PaperSize.A4)]
    [TestCase(PaperSize.A5)]
    public void GetPaperSizeInPixels_ValidPaperSizes_ReturnsCorrectDimensions(PaperSize paperSize)
    {
        // Arrange
        var expectedSizeCm = paperSize.GetPaperSizeInCentimeters();
        var expectedWidth = (double)new LengthConverter().ConvertFrom($"{expectedSizeCm.Width.ToString(CultureInfo.InvariantCulture)}cm")!;
        var expectedHeight = (double)new LengthConverter().ConvertFrom($"{expectedSizeCm.Height.ToString(CultureInfo.InvariantCulture)}cm")!;

        // Act
        var result = paperSize.GetPaperSizeInPixels();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Width, Is.EqualTo(expectedWidth).Within(0.01));
            Assert.That(result.Height, Is.EqualTo(expectedHeight).Within(0.01));
        });
    }
}