namespace CelestialMapper.UI.Test;

[TestFixture]
public class IntegerInputValidationTest : 
    NumberInputValidationTestBase<int, IntegerInputValidationTest>,
    INumberInputValidationTestBase<int>
{

    protected override InputMode InputMode => InputMode.Int;

    public static NumberInputValidationTestCaseData<int>[] NumberInputValidationTestCaseDataSource => new NumberInputValidationTestCaseData<int>[]
    {
        new("10", 10, false, 0, 11, true)
    };

    protected override NumberInputValidation<int> CreateNumberInputValidation(int MaxLength, bool AllowEmptyInput, int MinValue, int MaxValue)
    {
        return new IntegerInputValidation(ServiceProvider.Object)
        {
            MaxLength = MaxLength,
            AllowEmptyInput = AllowEmptyInput,
            MinValue = MinValue,
            MaxValue = MaxValue
        };
    }
}
