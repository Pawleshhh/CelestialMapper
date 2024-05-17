using CelestialMapper.Common;
using CelestialMapper.Core;
using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Infrastructure.Map;
using Moq;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class MapViewModelTest : ViewModelTest<MapViewModel>
{

    public Mock<IMapManager> MapManager { get; set; } = new();

    public Mock<ITimeMachineManager> TimeMachineManager { get; set; } = new();

    public override Func<MapViewModel> CreateSUT => () => new MapViewModel(
        MapManager.Object,
        TimeMachineManager.Object, 
        ViewModelSupport.Object);

    public override string DefaultFeatureName => "Map";

    #region SetUp

    [SetUp]
    public void SetUp()
    {
        MapManager = new Mock<IMapManager>(MockBehavior.Strict);
        TimeMachineManager = new Mock<ITimeMachineManager>(MockBehavior.Strict);
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

        var dateTime = new DateTime(2024, 1, 4);
        var location = new Geographic(10, 15);
        TimeMachineManager.SetupGet(x => x.DateTime)
            .Returns(dateTime);
        TimeMachineManager.SetupGet(x => x.Location)
            .Returns(location);

        MapManager
            .Setup(x => x.Generate(
                It.IsAny<Geographic>(),
                It.IsAny<DateTime>(), 
                It.IsAny<IGenerateMapSettings>()))
            .Returns(Task.FromResult(map));

        var sut = CreateSUTAndInitialize();

        // Act
        sut.GenerateMapCommand!.Execute(null);

        // Assert
        Assert.That(sut.CelestialObjects, Is.SameAs(map.CelestialObjects));
    }

    [Test]
    public void Constellations_GetsConstellations_ReturnsExpectedConstellations()
    {
        // Arrange
        IMap map = new CelestialMap(Array.Empty<CelestialObject>())
        {
            Constellations = new Constellation[]
            {
                new(1, "Name1", "Short1", new ConstellationLine[] { new(new(1, 1), new(1, 1))}),
                new(2, "Name2", "Short2", new ConstellationLine[] { new(new(2, 2), new(2, 2))}),
                new(3, "Name3", "Short3", new ConstellationLine[] { new(new(3, 3), new(3, 3))}),
                new(4, "Name4", "Short4", new ConstellationLine[] { new(new(4, 4), new(4, 4))}),
            }.ToHashSet()
        };

        var dateTime = new DateTime(2024, 1, 4);
        var location = new Geographic(10, 15);
        TimeMachineManager.SetupGet(x => x.DateTime)
            .Returns(dateTime);
        TimeMachineManager.SetupGet(x => x.Location)
            .Returns(location);

        MapManager
            .Setup(x => x.Generate(
                It.IsAny<Geographic>(),
                It.IsAny<DateTime>(),
                It.IsAny<IGenerateMapSettings>()))
            .Returns(Task.FromResult(map));

        var sut = CreateSUTAndInitialize();

        // Act
        sut.GenerateMapCommand!.Execute(null);

        // Assert
        Assert.That(sut.Constellations, Is.SameAs(map.Constellations));
    }

    [Test]
    public void GenerateMap_When_TimeMachineUpdated()
    {
        // Arrange
        IMap map = new CelestialMap(Array.Empty<CelestialObject>());

        MapManager
            .Setup(x => x.Generate(
                It.IsAny<Geographic>(),
                It.IsAny<DateTime>(),
                It.IsAny<IGenerateMapSettings>()))
            .Returns(Task.FromResult(Mock.Of<IMap>()));

        var dateTime = new DateTime(2024, 1, 4);
        var location = new Geographic(10, 15);
        TimeMachineManager.SetupGet(x => x.DateTime)
            .Returns(new DateTime());
        TimeMachineManager.SetupGet(x => x.Location)
            .Returns(new Geographic(0, 0));

        var sut = CreateSUTAndInitialize();

        // Act

        // Data changed so reset the setup of those getters
        TimeMachineManager.SetupGet(x => x.DateTime)
            .Returns(dateTime);
        TimeMachineManager.SetupGet(x => x.Location)
            .Returns(location);

        TimeMachineManager.Raise(
            x => x.TimeMachineUpdated += null,
            new PlatformEventArgs<(DateTime DateTime, Geographic Location)>((dateTime, location)));


        // Assert
        MapManager
            .Verify(x => x.Generate(
                location,
                dateTime.ToUniversalTime(),
                It.IsAny<IGenerateMapSettings>()), Times.Once);
    }

    #endregion

}
