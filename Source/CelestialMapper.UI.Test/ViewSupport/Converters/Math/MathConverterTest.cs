namespace CelestialMapper.UI.Test;

[TestFixture]
public class MathConverterTest
{

    [TestCase(2, 2, 4)]
    [TestCase(10, 2, 12)]
    [TestCase(5, 10, 15)]
    public void Convert_DivideDoubleBy_ReturnsExpectedValue(int x, int y, int expected)
    {
        // Arrange
        var conv = new MathConverterImplTest();

        // Act
        var result = conv.Convert(x, null!, y, null!);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(2, 2, 3, 5)]
    [TestCase(10, 2, 3, 13)]
    [TestCase(5, 10, 3, 8)]
    public void Convert_DivideDoubleBy_SetParameter_ReturnsExpectedValue(int x, int y, int p, int expected)
    {
        // Arrange
        var conv = new MathConverterImplTest();
        conv.Parameter = p;

        // Act
        var result = conv.Convert(x, null!, y, null!);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(2, 2, 0)]
    [TestCase(10, 2, 8)]
    [TestCase(5, 10, -5)]
    public void ConvertBack_DivideDoubleBy_ReturnsExpectedValue(int x, int y, int expected)
    {
        // Arrange
        var conv = new MathConverterImplTest();

        // Act
        var result = conv.ConvertBack(x, null!, y, null!);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(2, 2, 3, -1)]
    [TestCase(10, 2, 3, 7)]
    [TestCase(5, 10, 3, 2)]
    public void ConvertBack_DivideDoubleBy_SetParameter_ReturnsExpectedValue(int x, int y, int p, int expected)
    {
        // Arrange
        var conv = new MathConverterImplTest();
        conv.Parameter = p;

        // Act
        var result = conv.ConvertBack(x, null!, y, null!);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    private class MathConverterImplTest : MathConverter<int>
    {
        protected override Func<int?, int?, int?> ConvertValue => (v, p) => v + p;

        protected override Func<int?, int?, int?> ConvertValueBack => (v, p) => v - p;
    }
}
