namespace CelestialMapper.Core.Test;

public class DatabaseManagerTest
{
    //[TestCase()]
    public async Task GetCelestialObjects_Test(
        GeographicCoordinates geographicCoordinates, 
        double magnitude, 
        long[] expectedIds)
    {
        // ARRANGE
        DateTime dateTime = new DateTime();
        var dbManager = new DatabaseManager("./stars.sqlite");

        // ACT
        var result = await dbManager.GetCelestialObjects(dateTime, geographicCoordinates, magnitude);

        // ASSERT
        Assert.Multiple(() =>
        {
            foreach (var id in result.Select(x => x.Id))
            {
                CollectionAssert.Contains(expectedIds, id);
            }
        });
    }

}