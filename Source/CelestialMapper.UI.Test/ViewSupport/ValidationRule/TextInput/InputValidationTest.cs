namespace CelestialMapper.UI.Test;

[TestFixture]
public class InputValidationTest : ValidationRuleTestBase
{

    [TestCase("Input", 6, true)]
    [TestCase("Input", 5, true)]
    [TestCase("Input", 4, false)]
    public void IsCorrect_WithSetupInputValidation_ReturnsExpectedValue(string input, int maxLength, bool expected)
    {
        // Arrange
        var sut = new InputValidationMock(ServiceProvider.Object)
        {
            MaxLength = maxLength
        };

        // Act
        var result = sut.IsCorrect(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

}

public class InputValidationMock : InputValidation
{
    public override InputMode Mode { get; }

    public InputValidationMock(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        
    }

}