using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using CelestialMapper.TestUtilities;
using Moq;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class MapViewModelTest : ViewModelTest<MapViewModel>
{

    public Mock<IMapManager> MapManager { get; set; } = new();

    public override Func<MapViewModel> CreateSUT => () => new MapViewModel(MapManager.Object, ViewModelSupport.Object);

    public override string DefaultFeatureName => "Map";

    #region SetUp

    [SetUp]
    public void SetUp()
    {
        MapManager = new Mock<IMapManager>(MockBehavior.Strict);
    }

    #endregion

    #region Tests

    [Test]
    public void Initialize_InitializeGenerateMapCommand_GenerateMapCommandIsNotNull()
    {
        // Arrange & Act
        var sut = CreateSUTAndInitialize();

        // Assert
        Assert.That(sut.GenerateMapCommand, Is.Not.Null);
    }

    [Test]
    public void CelestialObjects_GetsCelestialObjects_ReturnsExpectedCelestialObjects()
    {
        // Arrange
        IMap map = new CelestialMap(new CelestialObject[]
        {
            new(1, "Name1", new(1, 1), 1, "HR"),
            new(2, "Name2", new(2, 2), 2, "HR"),
            new(3, "Name3", new(3, 3), 3, "HR"),
            new(4, "Name4", new(4, 4), 4, "HR"),
        });

        MapManager
            .Setup(x => x.Generate(It.IsAny<Geographic>(), It.IsAny<DateTime>(), It.IsAny<IGenerateMapSettings>()))
            .Returns(Task.FromResult(map));

        var sut = CreateSUTAndInitialize();

        // Act
        sut.GenerateMapCommand!.Execute(null);

        // Assert
        Assert.That(sut.CelestialObjects, Is.SameAs(map.CelestialObjects));
    }

    #endregion

}
