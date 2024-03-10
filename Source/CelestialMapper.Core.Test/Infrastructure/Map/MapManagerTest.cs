using CelestialMapper.Common;
using CelestialMapper.Core.Database;
using CelestialMapper.Core.Infrastructure.Map;
using CelestialMapper.TestUtilities;
using Moq;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Test;

[TestFixture]
internal class MapManagerTest : TestBase<MapManager>
{

    #region Prepare Tests

    public override Func<MapManager> CreateSUT => () => new(this.celestialDataBaseMock.Object);

    private Mock<ICelestialDatabase> celestialDataBaseMock = new();

    [SetUp]
    public void SetUp()
    {
        this.celestialDataBaseMock.Reset();
    }

    #endregion

    #region Tests

    [Test]
    public async Task Generate_TakesParameters_CreatesExpectedMap()
    {
        // Arrange
        var location = new Geographic(15, 54);
        var dateTime = new DateTime(2024, 03, 10);
        var magnitudeRange = NumRange.Of(0d, 4);
        var mapSettings = IGenerateMapSettings.Create(magnitudeRange);

        var sut = CreateSUT();

        // Act
        var map = await sut.Generate(location, dateTime, mapSettings);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(map.Location, Is.EqualTo(location));
            Assert.That(map.DateTime, Is.EqualTo(dateTime));
            Assert.That(map.GenerateMapSettings.MagnitudeRange, Is.EqualTo(magnitudeRange));
        });
    }

    #endregion

}
