namespace CelestialMapper.ViewModel;

[TestFixture]
public class FeatureNameTest
{

    [Test]
    public void CreateFeatureName_AllPropertiesInitialized_AsExpected()
    {
        var featureName = "Feature";

        var result = new FeatureName(featureName);

        Assert.That(result.Name, Is.EqualTo(featureName));
        Assert.That(result.ViewName, Is.EqualTo(featureName + "View"));
    }

    [TestCase(false)]
    [TestCase(true)]
    public void IsUnknown_ChecksIfIsUnknown_AndReturnsGivenResult(bool isUnknown)
    {
        var featureName = isUnknown
            ? FeatureName.Unknown
            : new FeatureName("SomeFeature");

        Assert.That(featureName.IsUnknown(), Is.EqualTo(isUnknown));
    }

}
