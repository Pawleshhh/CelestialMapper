using CelestialMapper.TestUtilities;

namespace CelestialMapper.Common.Test;

internal class ArrayExtensionsTest
{

    #region Tests

    [TestCase(0)]
    [TestCase(2)]
    [TestCase(4)]
    public void As_CastsObjectAtGivenIndexOfArray_ReturnsExpectedObject(int index)
    {
        // Arrange
        object[] array = Enumerable.Range(0, 5)
            .Select(i => new DummyClass())
            .ToArray();

        // Act
        DummyClass dummyClass = array.As<DummyClass>(index);

        // Assert
        Assert.That(dummyClass, Is.SameAs(array[index]));
    }

    [Test]
    public void As_IndexWithNotExpectedType_ThrowsException()
    {
        // Arrange
        object[] array = new object[3]
        {
            new DummyClass(),
            new object(),
            new DummyClass(),
        };

        // Act & Assert
        Assert.Throws<InvalidCastException>(() => array.As<DummyClass>(1));
    }

    #endregion

}
