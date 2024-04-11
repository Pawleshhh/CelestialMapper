using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class ValidationRuleTest
{

    #region SetUp

    public Mock<IServiceProvider> ServiceProvider { get; set; } = new();

    public Mock<IResourceResolver> ResourceResolver { get; set; } = new(MockBehavior.Strict);

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ServiceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
        ServiceProvider.Setup(x => x.GetService(typeof(IResourceResolver)))
            .Returns(ResourceResolver.Object);

        string result = string.Empty;
        ResourceResolver
            .Setup(x => x.TryResolveString(It.IsAny<string>(), out result))
            .Returns(false);
    }

    #endregion

    #region Tests

    [Test]
    public void Validate_ValueIsNotExpectedType_NoInvalidResultString_ReturnsInvalidResultWithNullContent()
    {
        // Arrange
        var sut = new ValidationRuleMock<int>(ServiceProvider.Object);

        // Act
        var result = sut.Validate("value", null!);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorContent, Is.Null);
        });
    }


    [Test]
    public void Validate_ValueIsNotExpectedType_WithInvalidResultString_ReturnsInvalidResultWithNullContent()
    {
        // Arrange
        string key = "String.SomeError.Content";
        var sut = new ValidationRuleMock<int>(ServiceProvider.Object)
        {
            InvalidResultString = key
        };

        string resolvedString = "Error content";
        ResourceResolver
            .Setup(x => x.TryResolveString(key, out resolvedString))
            .Returns(true);

        // Act
        var result = sut.Validate("value", null!);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorContent, Is.EqualTo(resolvedString));
        });
    }

    [Test]
    public void Validate_ValueIsExpectedType_NotCorrect_NoInvalidResultString_ReturnsInvalidResultWithNullContent()
    {
        // Arrange
        var sut = new ValidationRuleMock<int>(ServiceProvider.Object)
        {
            IsCorrectReturns = false
        };

        // Act
        var result = sut.Validate(10, null!);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorContent, Is.Null);
        });
    }

    [Test]
    public void Validate_ValueIsNotExpectedType_NotCorrect_WithInvalidResultString_ReturnsInvalidResultWithNullContent()
    {
        // Arrange
        string key = "String.SomeError.Content";
        var sut = new ValidationRuleMock<int>(ServiceProvider.Object)
        {
            IsCorrectReturns = false,
            InvalidResultString = key
        };

        string resolvedString = "Error content";
        ResourceResolver
            .Setup(x => x.TryResolveString(key, out resolvedString))
            .Returns(true);

        // Act
        var result = sut.Validate(10, null!);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorContent, Is.EqualTo(resolvedString));
        });
    }

    [Test]
    public void Validate_ValueIsExpectedType_Correct_ReturnsValidResult()
    {
        // Arrange
        var sut = new ValidationRuleMock<int>(ServiceProvider.Object)
        {
            IsCorrectReturns = true
        };

        // Act
        var result = sut.Validate(10, null!);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorContent, Is.Null);
        });
    }

    #endregion

}

public class ValidationRuleMock<T> : ValidationRule<T>
{

    public ValidationRuleMock(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        
    }

    public bool IsCorrectReturns { get; set; }

    public override bool IsCorrect(T value)
    {
        return IsCorrectReturns;
    }
}
