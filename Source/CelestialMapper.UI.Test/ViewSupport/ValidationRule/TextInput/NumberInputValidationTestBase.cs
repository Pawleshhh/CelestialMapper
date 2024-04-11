using System.Numerics;

namespace CelestialMapper.UI.Test;

public interface INumberInputValidationTestBase<TNumber>
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>, IParsable<TNumber>
{
    public static abstract NumberInputValidationTestCaseData<TNumber>[] NumberInputValidationTestCaseDataSource { get; }
}

public abstract class NumberInputValidationTestBase<TNumber, TTestClass> : ValidationRuleTestBase
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>, IParsable<TNumber>
    where TTestClass : NumberInputValidationTestBase<TNumber, TTestClass>, INumberInputValidationTestBase<TNumber>
{
    protected abstract InputMode InputMode { get; }

    [Test]
    public void InputMode_GetsMode_IsAsExpected()
    {
        // Arrange
        var sut = CreateNumberInputValidation(100, false, TNumber.MinValue, TNumber.MaxValue);

        // Act
        var inputMode = sut.Mode;

        // Assert
        Assert.That(inputMode, Is.EqualTo(InputMode));
    }

    public static NumberInputValidationTestCaseData<TNumber>[] IsCorrectTestCaseDataSource
        => TTestClass.NumberInputValidationTestCaseDataSource;

    [Test, TestCaseSource(nameof(IsCorrectTestCaseDataSource))]
    public void IsCorrect_WithGivenNumberInputValidation_ReturnsExpectedResult(
        NumberInputValidationTestCaseData<TNumber> testCase)
    {
        // Arrange
        var sut = CreateNumberInputValidation(
            testCase.MaxLength, 
            testCase.AllowEmptyInput, 
            testCase.MinValue,
            testCase.MaxValue);

        // Act
        var result = sut.IsCorrect(testCase.Input);

        // Assert
        Assert.That(result, Is.EqualTo(testCase.ExpectedResult));
    }

    protected abstract NumberInputValidation<TNumber> CreateNumberInputValidation(
        int MaxLength,
        bool AllowEmptyInput,
        TNumber MinValue,
        TNumber MaxValue);

}

public record NumberInputValidationTestCaseData<TNumber>(
    string Input,
    int MaxLength,
    bool AllowEmptyInput,
    TNumber MinValue,
    TNumber MaxValue,
    bool ExpectedResult)
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>, IParsable<TNumber>
{

}