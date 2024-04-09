using CelestialMapper.Common;
using CelestialMapper.TestUtilities;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Test;

[TestFixture]
public class TimeMachineManagerTest : TestBase<TimeMachineManager>
{

    #region SetUp

    public override Func<TimeMachineManager> CreateSUT => () => new();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {

    }

    #endregion

    #region Tests

    [Test]
    public void DateTime_SetsDateTime_GetsExpectedDateTime()
    {
        // Arrange
        var dateTime = new DateTime(2024, 3, 3);
        var sut = CreateSUT();

        // Act
        sut.DateTime = dateTime;

        // Assert
        Assert.That(sut.DateTime, Is.EqualTo(dateTime));
    }

    [Test]
    public void DateTime_SetsDateTime_DateTimeChangedRised()
    {
        // Arrange
        var dateTime = new DateTime(2024, 3, 3);
        var sut = CreateSUT();

        PlatformEventArgs<ITimeMachineManager, DateTime>? eventArgs = null;
        sut.DateTimeChanged += e => eventArgs = e;

        // Act
        sut.DateTime = dateTime;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventArgs, Is.Not.Null);
            Assert.That(eventArgs!.Sender, Is.SameAs(sut));
            Assert.That(eventArgs!.Data, Is.EqualTo(dateTime));
        });
    }

    [Test]
    public void DateTime_SetsSameDateTime_DateTimeChangedRised()
    {
        // Arrange
        var dateTime = new DateTime(2024, 3, 3);
        var sut = CreateSUT();
        sut.DateTime = dateTime;

        PlatformEventArgs<ITimeMachineManager, DateTime>? eventArgs = null;
        sut.DateTimeChanged += e => eventArgs = e;

        // Act
        sut.DateTime = dateTime;

        // Assert
        Assert.That(eventArgs, Is.Null);
    }

    [Test]
    public void Location_SetsLocation_GetsExpectedLocation()
    {
        // Arrange
        var location = new Geographic(30, 11);
        var sut = CreateSUT();

        // Act
        sut.Location = location;

        // Assert
        Assert.That(sut.Location, Is.EqualTo(location));
    }

    [Test]
    public void Location_SetsLocation_LocationChangedRised()
    {
        // Arrange
        var location = new Geographic(31, 25);
        var sut = CreateSUT();

        PlatformEventArgs<ITimeMachineManager, Geographic>? eventArgs = null;
        sut.LocationChanged += e => eventArgs = e;

        // Act
        sut.Location = location;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventArgs, Is.Not.Null);
            Assert.That(eventArgs!.Sender, Is.SameAs(sut));
            Assert.That(eventArgs!.Data, Is.EqualTo(location));
        });
    }

    [Test]
    public void Location_SetsSameLocation_LocationChangedRised()
    {
        // Arrange
        var location = new Geographic(11, 14);
        var sut = CreateSUT();
        sut.Location = location;

        PlatformEventArgs<ITimeMachineManager, Geographic>? eventArgs = null;
        sut.LocationChanged += e => eventArgs = e;

        // Act
        sut.Location = location;

        // Assert
        Assert.That(eventArgs, Is.Null);
    }

    [Test]
    public void Update_UpdatesTimeMachine_DateAndLocationChangedWithEventRised()
    {
        // Arrange
        var dateTime = new DateTime(2024, 11, 24);
        var location = new Geographic(11, 53);
        var sut = CreateSUT();

        PlatformEventArgs<ITimeMachineManager, (DateTime DateTime, Geographic Location)>? timeMachineUpdatedEventArgs = null;
        object? dateTimeUpdatedEventArgs = null;
        object? locationUpdatedEventArgs = null;
        sut.TimeMachineUpdated += e => timeMachineUpdatedEventArgs = e;
        sut.DateTimeChanged += e => dateTimeUpdatedEventArgs = e;
        sut.LocationChanged += e => locationUpdatedEventArgs = e;

        // Act
        sut.Update(dateTime, location);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sut.DateTime, Is.EqualTo(dateTime));
            Assert.That(sut.Location, Is.EqualTo(location));

            Assert.That(timeMachineUpdatedEventArgs, Is.Not.Null);
            Assert.That(timeMachineUpdatedEventArgs!.Sender, Is.SameAs(sut));
            Assert.That(timeMachineUpdatedEventArgs!.Data.DateTime, Is.EqualTo(dateTime));
            Assert.That(timeMachineUpdatedEventArgs!.Data.Location, Is.EqualTo(location));

            Assert.That(dateTimeUpdatedEventArgs, Is.Null);
            Assert.That(locationUpdatedEventArgs, Is.Null);
        });
    }

    #endregion

}
