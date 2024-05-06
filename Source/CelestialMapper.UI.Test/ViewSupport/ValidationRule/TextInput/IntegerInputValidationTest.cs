namespace CelestialMapper.UI.Test;

[TestFixture]
public class IntegerInputValidationTest : 
    NumberInputValidationTestBase<int, IntegerInputValidationTest>,
    INumberInputValidationTestSource<int>
{

    protected override InputMode InputMode => InputMode.Int;

    public static NumberInputValidationTestCaseData<int>[] NumberInputValidationTestCaseDataSource => new NumberInputValidationTestCaseData<int>[]
    {
        new("10", 10, false, 0, 11, true),
        new("-999", 5, false, -1000, -998, true),
        new("42", 10, false, 43, 45, false),
        new("459", 2, false, int.MinValue, int.MaxValue, false),
        new("abc", 4, false, int.MinValue, int.MaxValue, false),
        new("0", 1, true, -100, 100, true),
        new("12345", 5, false, 0, 99999, true),
        new("-500", 4, false, -1000, 0, true),
        new("1001", 4, false, 1, 1000, false),
        new("abcde", 5, false, -10, 10, false),
        new("9999", 4, false, 9998, 10000, true),
        new("-1000", 5, false, -1001, -999, true),
        new("0", 1, true, 0, 0, true),
        new("100000", 6, false, 0, 99999, false),
        new("-100000", 7, false, -99999, 0, false),
        new("999999999", 18, false, 0, 999999998, false),
        new("12345678901234567890", 20, false, 0, 999999999, false),
        new("-98765432109876543210", 21, false, -999999999, 0, false),

        new("", 3, false, 1, 100, false),
        new("", 3, true, 1, 100, true),
        new(" ", 1, false, int.MinValue, int.MaxValue, false),
        new("     ", 5, false, int.MinValue, int.MaxValue, false)
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
