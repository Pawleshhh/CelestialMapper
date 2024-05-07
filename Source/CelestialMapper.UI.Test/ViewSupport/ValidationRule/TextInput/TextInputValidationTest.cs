namespace CelestialMapper.UI.Test;

[TestFixture]
public class TextInputValidationTest : ValidationRuleTestBase
{

    #region Tests

    [TestCase("", 10    , false, false, false)]
    [TestCase("", 10, false, true, false)]
    [TestCase(" ", 10, false, false, false)]
    [TestCase(" ", 10, false, true, false)]
    [TestCase("abc", 10, false, false, true)]
    [TestCase("abc", 10, true, false, true)]
    [TestCase("abc", 10, false, true, false)]
    [TestCase("abc", 4, false, false, true)]
    [TestCase("abc", 3, false, false, true)]
    [TestCase("abc", 2, false, false, false)]
    [TestCase("123", 10, false, true, true)]
    [TestCase("123", 3, false, true, true)]
    [TestCase("123", 2, false, true, false)]
    public void IsCorrect_WithGivenInput_ReturnIfIsCorrectOrNot(string input, int maxLength, bool allowEmptyInput, bool digitsOnly, bool shouldBeCorrect)
    {
        // Arrange
        var validation = new TextInputValidation(ServiceProvider.Object)
        {
            MaxLength = maxLength,
            AllowEmptyInput = allowEmptyInput,
            DigitsOnly = digitsOnly,
        };

        // Act
        var result = validation.IsCorrect(input);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeCorrect));
    }

    #endregion

}
