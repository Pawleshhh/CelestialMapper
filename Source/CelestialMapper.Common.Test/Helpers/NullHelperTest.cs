using CelestialMapper.TestUtilities;

namespace CelestialMapper.Common.Test;

[TestFixture]
public class NullHelperTest
{

    #region Tests

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void FirstNotNull_TakesFirstObjectFromArrayThatIsNotNull_ReturnsNonNullObject(int notNullIndex)
    {
        // Assert
        const int length = 4;
        object?[] array = new object[length];
        array[notNullIndex] = new();

        // Act
        var result = NullHelper.FirstNotNull(array);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void FirstNotNull_AllObjectsAreNull_ThrowsInvalidOperationException()
    {
        // Assert
        object?[] array = new object[3];

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => 
            NullHelper.FirstNotNull(array));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void FirstNotNullOrDefault_TakesFirstObjectFromArrayThatIsNotNull_ReturnsNonNullObject(int notNullIndex)
    {
        // Assert
        const int length = 4;
        DummyClass?[] array = new DummyClass[length];
        array[notNullIndex] = new();

        // Act
        var result = NullHelper.FirstNotNull(array);

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void FirstNotNullOrDefault_AllObjectsAreNull_ReturnsDefaultObject()
    {
        // Assert
        DummyClass?[] array = new DummyClass[3];

        // Act
        var result = NullHelper.FirstNotNullOrDefault(array);

        // Assert
        Assert.That(result, Is.SameAs(default));
    }

    #endregion

}
