namespace CelestialMapper.UI;

public class TextInputValidation : InputValidation
{

    public TextInputValidation()
    {
        
    }

    public TextInputValidation(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        
    }

    public override InputMode Mode => InputMode.String;

    public bool DigitsOnly { get; set; }

    public override bool IsCorrect(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return AllowEmptyInput;
        }

        if (!base.IsCorrect(text))
        {
            return false;
        }

        if (!CheckDigitsOnly(text))
        {
            return false;
        }

        return true;
    }

    protected virtual bool CheckDigitsOnly(string text)
    {
        if (!DigitsOnly)
        {
            return true;
        }

        foreach (char c in text)
        {
            if (c < '0' || c > '9')
            {
                return false;
            }
        }

        return true;
    }

}
