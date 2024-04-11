namespace CelestialMapper.UI;

public class IntegerInputValidation : NumberInputValidation<int>
{
    public override InputMode Mode => InputMode.Int;

    public IntegerInputValidation()
        : base()
    {
        
    }

    public IntegerInputValidation(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        
    }
}
