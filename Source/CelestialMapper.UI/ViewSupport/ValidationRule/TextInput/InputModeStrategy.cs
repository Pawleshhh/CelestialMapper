﻿using System.Globalization;

namespace CelestialMapper.UI;

public abstract class InputModeStrategy : ValidationRule<string>
{

    public abstract InputMode Mode { get; }

    public int MaxLength { get; set; } = int.MaxValue;

    public override bool IsCorrect(string text)
    {
        if (!IsLengthCorrect(text.Length))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsLengthCorrect(int length)
        => length <= MaxLength;

}
