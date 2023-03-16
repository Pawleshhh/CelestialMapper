namespace CelestialMapper.Core.Test.Database;

public class DatabaseManagerTest
{
    [TestCase(53.4, 14.5, 1, new long[] {1, 2, 3})]
    public async Task GetCelestialObjects_Test(
        double latitude, double longitude,
        double magnitude,
        long[] expectedIds)
    {
        // ARRANGE
        DateTime dateTime = DateTime.Now;
        var dbManager = new DatabaseManager("./Database/stars_test.sqlite");

        // ACT
        var result = await dbManager.GetCelestialObjects(dateTime, new(latitude, longitude), magnitude);

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