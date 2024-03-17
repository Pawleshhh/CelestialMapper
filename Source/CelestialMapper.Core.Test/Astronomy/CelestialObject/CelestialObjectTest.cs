using CelestialMapper.Core.Astronomy;
using CelestialMapper.Core.Database;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Test;

[TestFixture]
public class CelestialObjectTest
{

    [Test]
    public void FromStarDataRow_WithGivenData_ReturnsCelestialObject()
    {
        // Arrange
        var starDataRow = new StarDataRow
        { 
            Id = 5,
            Ra = TimeSpan.FromHours(4.201).TotalHours,
            Dec = 30,
            Mag = 1.2,
            Proper = null!,
            Bf = null!,
            Gl = null!,
            Hr = "HrName",
            Hd = "HdName"
        };
        var dateTime = new DateTime(2024, 01, 01);
        var location = new Geographic(15, 15);

        // Act
        var result = CelestialObject.FromStarDataRow(location, dateTime, starDataRow);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.HorizonCoordinates, Is.EqualTo(new Horizon(296.8014014550579, 40.00358317695597)));
            Assert.That(result.Id, Is.EqualTo(5));
            Assert.That(result.Name, Is.EqualTo("HrName"));
            Assert.That(result.Magnitude, Is.EqualTo(1.2));
        });
    }

}
