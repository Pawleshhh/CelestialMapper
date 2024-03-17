using CelestialMapper.Core.Database.CustomFunctions;

namespace CelestialMapper.Core.Test;

[TestFixture]
public class SkyContainsFunctionTest
{

    [Test]
    public void SkyContainsFunction_InvokeWithGivenArguments_ExpectsTrue()
    {
        // Arrange
        var ra = 4.32;
        var dec = 34.3;
        var date = "01/01/2024 21:37:01";
        var lat = 15.4;
        var lon = 32.1;

        var skyContainsFunc = new SkyContainsFunction();

        // Act
        var result = (bool)skyContainsFunc
            .Invoke(new object[] { ra, dec, date, lat, lon });

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void SkyContainsFunction_InvokeWithGivenArguments_ExpectsFalse()
    {
        // Arrange
        var ra = 12.32;
        var dec = 34.3;
        var date = "01/01/2024 21:37:01";
        var lat = 15.4;
        var lon = -61.1;

        var skyContainsFunc = new SkyContainsFunction();

        // Act
        var result = (bool)skyContainsFunc
            .Invoke(new object[] { ra, dec, date, lat, lon });

        // Assert
        Assert.IsFalse(result);
    }

}
