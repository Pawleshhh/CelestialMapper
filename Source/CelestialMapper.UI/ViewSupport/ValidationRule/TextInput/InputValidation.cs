﻿namespace CelestialMapper.UI;

public abstract class InputValidation : ValidationRule<string>
{

    public abstract InputMode Mode { get; }

    public int MaxLength { get; set; } = int.MaxValue;

    public bool AllowEmptyInput { get; set; } = true;

    public InputValidation()
        : base()
    {
        
    }

    public InputValidation(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        
    }

    public override bool IsCorrect(string text)
    {
        if (text.IsNullOrEmpty())
        {
            return AllowEmptyInput;
        }

        if (!IsLengthCorrect(text.Length))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsLengthCorrect(int length)
        => length <= MaxLength;

}
