using CelestialMapper.Common;
using CelestialMapper.Core.Database.CustomFunctions;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Test;

[TestFixture]
public class AstronomyCoordsHelperTest
{
    [Test]
    public void MapCartesianCoords_Returns_Correct_Values()
    {
        Horizon horizon = new Horizon(45, 45);
        double mapDiameter = 100;

        double expectedX = 25 * MathHelper.SinD(45);
        double expectedY = 25 * MathHelper.CosD(45);

        var result = AstronomyCoordsHelper.MapCartesianCoords(horizon, mapDiameter);

        Assert.That(result.X, Is.EqualTo(expectedX).Within(0.000000001));
        Assert.That(result.Y, Is.EqualTo(expectedY).Within(0.000000001));
    }

    [Test]
    public void SkyContains_WithGivenArguments_ExpectsTrue()
    {
        // Arrange
        var ra = 4.32;
        var dec = 34.3;
        var date = new DateTime(2024, 1, 1, 21, 37, 1);
        var location = new Geographic(15.4, 32.1);

        var hourAngle = PracticalAstro.CoordinateSystems
            .RightAscensionToHourAngle(date, location.Longitude, ra * 15d);

        // Act
        var result = AstronomyCoordsHelper.SkyContains(new(hourAngle, dec), location);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void SkyContains_WithGivenArguments_ExpectsFalse()
    {
        // Arrange
        var ra = 12.32;
        var dec = 34.3;
        var date = new DateTime(2024, 1, 1, 21, 37, 1);
        var location = new Geographic(15.4, -61.1);

        var hourAngle = PracticalAstro.CoordinateSystems
            .RightAscensionToHourAngle(date, location.Longitude, ra * 15d);

        // Act
        var result = AstronomyCoordsHelper.SkyContains(new(hourAngle, dec), location);

        // Assert
        Assert.IsFalse(result);
    }

}
