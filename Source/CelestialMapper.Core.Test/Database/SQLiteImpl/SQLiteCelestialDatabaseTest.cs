using CelestialMapper.Common;
using CelestialMapper.Core.Database;
using CelestialMapper.Core.Database.SQLiteImpl;
using CelestialMapper.TestUtilities;
using Moq;
using PracticalAstronomy.CSharp;
using System.Data.Common;
using System.Data.SQLite;

namespace CelestialMapper.Core.Test;

[TestFixture]
internal class SQLiteCelestialDatabaseTest : TestBase<SQLiteCelestialDatabase>
{

    #region Prepare Tests

    private const string DataSourcePath = "./Path/To/DataBase.sqlite";

    private readonly SQLiteConnectionStringBuilder connectionStringBuilder = new()
    {
        DataSource = $"Data Source ={DataSourcePath}"
    };

    private Mock<IDatabaseWrapper> wrapper = new();
    private Mock<ICelestialObjectProcessor> celestialObjectProcessor = new();
    private int processorCount;

    public override Func<SQLiteCelestialDatabase> CreateSUT => () => new(
            this.wrapper.Object,
            this.celestialObjectProcessor.Object,
            this.connectionStringBuilder,
            () => this.processorCount);

    [SetUp]
    public void SetUp()
    {
        this.processorCount = 2;
        this.wrapper = new Mock<IDatabaseWrapper>();
        this.wrapper
            .Setup(x => x.CreateDbConnection(this.connectionStringBuilder))
            .Returns(Mock.Of<DbConnection>());

        this.celestialObjectProcessor = new Mock<ICelestialObjectProcessor>();
    }

    #endregion

    #region Tests

    [Test]
    public void Constructor_NullDbWrapper_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => new SQLiteCelestialDatabase(
                null!,
                this.celestialObjectProcessor.Object,
                null,
                null));
    }

    [Test]
    public void Constructor_NullCelestialObjectProcessor_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => new SQLiteCelestialDatabase(
                this.wrapper.Object,
                null!,
                null,
                null));
    }

    [Test]
    public void GetCelestialObjects_CreatesDbConnectionOpensAndClosesIt_Passes()
    {
        // Arrange
        Geographic location = new(0, 0);
        DateTime dateTime = new(2025, 1, 5);

        var dbConnectionMock = new Mock<DbConnection>();
        dbConnectionMock.Setup(c => c.Open());
        dbConnectionMock.Setup(c => c.Close());

        this.wrapper
            .Setup(x => x.CreateDbConnection(this.connectionStringBuilder))
            .Returns(dbConnectionMock.Object);

        var sut = CreateSUT();

        // Act
        _ = sut.GetCelestialObjects(location, dateTime);

        // Assert
        Assert.Multiple(() =>
        {
            this.wrapper
                .Verify(x => 
                    x.CreateDbConnection(this.connectionStringBuilder),
                    Times.Once);
            dbConnectionMock.Verify(x => x.Open(), Times.Once);
            dbConnectionMock.Verify(x => x.Close(), Times.Once);
        });
    }

    [Test]
    public void GetCelestialObjects_CreatesQuery_Passes()
    {
        // Arrange
        Geographic location = new(10, 14);
        DateTime dateTime = new(2025, 1, 5);
        var magnitude = NumRange.Of(3d, 12);

        string expectedQuery = "SELECT * FROM stars WHERE " +
            "mag BETWEEN 3 AND 12 " +
            "AND (90 - 10 + dec) >= 0 " +
            "AND SKYCONTAINS(ra, dec, '05/01/2025 00:00:00', 10, 14)";

        var sut = CreateSUT();

        // Act
        _ = sut.GetCelestialObjects(location, dateTime, magnitude);

        // Assert
        this.wrapper.Verify(x => x.Query<StarDataRow>(It.IsAny<DbConnection>(), expectedQuery), Times.Once);
    }

    [Test]
    public void GetCelestialObjects_ProcessRows_ReturnsCelestialObjects()
    {
        // Arrange
        Geographic location = new(10, 14);
        DateTime dateTime = new(2025, 1, 5);
        var magnitude = NumRange.Of(3d, 12);

        var starDataRows = new StarDataRow[]
        {
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 5 }
        };

        this.wrapper
            .Setup(x => x.Query<StarDataRow>(It.IsAny<DbConnection>(), It.IsAny<string>()))
            .Returns(starDataRows);

        var sut = CreateSUT();

        // Act
        var result = sut.GetCelestialObjects(location, dateTime, magnitude);

        // Assert
        Assert.Multiple(() =>
        {
            this.celestialObjectProcessor
                .Verify(x => x.Process(starDataRows, It.IsAny<Action<StarDataRow>>(), false), Times.Once);
            Assert.That(result, Is.Not.Null);
        });
    }

    [TestCase(1, 123, false)]
    [TestCase(2, 321, false)]
    [TestCase(1, 10_001, false)]
    [TestCase(2, 10_001, true)]
    public void GetCelestialObjects_ParallelProcessing_ParallelProcessingTrueOrFalse(int processorCount, int rowsNumber, bool parallelProcessing)
    {
        // Arrange
        Geographic location = new(10, 14);
        DateTime dateTime = new(2025, 1, 5);
        var magnitude = NumRange.Of(3d, 12);

        this.processorCount = processorCount;

        var starDataRows = Enumerable
            .Range(0, rowsNumber)
            .Select(i => new StarDataRow() { Id = i })
            .ToArray();

        this.wrapper
            .Setup(x => x.Query<StarDataRow>(It.IsAny<DbConnection>(), It.IsAny<string>()))
            .Returns(starDataRows);

        var sut = CreateSUT();

        // Act
        var result = sut.GetCelestialObjects(location, dateTime, magnitude);

        // Assert
        this.celestialObjectProcessor
            .Verify(x => 
                x.Process(
                    It.IsAny<IEnumerable<StarDataRow>>(), 
                    It.IsAny<Action<StarDataRow>>(), parallelProcessing), Times.Once);
    }

    #endregion

    #region Helpers

    #endregion

}
