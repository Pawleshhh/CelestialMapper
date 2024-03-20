using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using CelestialMapper.TestUtilities;

namespace CelestialMapper.Core.Test;

[TestFixture]
internal class CelestialMapTest : TestBase<CelestialMap>
{

    public override Func<CelestialMap> CreateSUT => () => new CelestialMap(new CelestialObject[0]);

    #region Test

    [TestCase(new long[] { 1, 2, 3, 4 }, 4)]
    [TestCase(new long[] { 1, 2, 2, 4 }, 3)]
    [TestCase(new long[] { 1, 1, 1, 4 }, 2)]
    [TestCase(new long[] { 1, 4, 4, 4 }, 2)]
    [TestCase(new long[] { 3, 3, 3, 3 }, 1)]
    public void Constructor_TakesCollectionOfCelestialObjects_KeepsNotDuplicatedOnes(long[] ids, int expectedCount)
    {
        // Arrange
        var celestialObjects = ids
            .Select(i => new CelestialObject(i, $"SomeName{i}", new(0, 0), 0, "hr"));
        
        // Act
        var celestialMap = new CelestialMap(celestialObjects);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(celestialMap.CelestialObjects.Count, Is.EqualTo(expectedCount));
            CollectionAssert.AllItemsAreUnique(celestialMap.CelestialObjects);
        });
    }

    [Test]
    public void GenerateMapSettings_GetsMapSettings_ReturnsMapSettingsWithDefaultMagnitude()
    {
        // Arrange
        var celestialMap = CreateSUT();

        // Act
        var settings = celestialMap.GenerateMapSettings;

        // Assert
        Assert.That(settings.MagnitudeRange, Is.EqualTo(MapConstants.DefaultMagnitudeRange));
    }

    [Test]
    public void Location_GetsGeographicLocation_ReturnsDefaultLocation()
    {
        // Arrange & Act
        var celestialMap = CreateSUT();

        // Assert
        Assert.That(celestialMap.Location, Is.EqualTo(MapConstants.DefaultLocation));
    }

    #endregion

}
