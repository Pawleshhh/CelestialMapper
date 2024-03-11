namespace CelestialMapper.Common;

public static class TimeSpanExtension
{

    #region Deconstructors

    public static void Deconstruct(this TimeSpan timeSpan, out int hour, out int minute, out int second)
    {
        hour = timeSpan.Hours;
        minute = timeSpan.Minutes;
        second = timeSpan.Seconds;
    }

    public static void Deconstruct(
        this TimeSpan timeSpan, 
        out int hour,
        out int minute, 
        out int second,
        out int millisecond)
    {
        Deconstruct(timeSpan, out hour, out minute, out second);
        millisecond = timeSpan.Milliseconds;
    }

    public static void Deconstruct(
        this TimeSpan timeSpan,
        out int hour,
        out int minute,
        out int second,
        out int millisecond,
        out int microsecond)
    {
        Deconstruct(timeSpan, out hour, out minute, out second, out millisecond);
        microsecond = timeSpan.Microseconds;
    }

    public static void Deconstruct(
        this TimeSpan timeSpan,
        out int hour,
        out int minute,
        out int second,
        out int millisecond,
        out int microsecond,
        out int nanosecond)
    {
        Deconstruct(timeSpan, out hour, out minute, out second, out millisecond, out microsecond);
        nanosecond = timeSpan.Nanoseconds;
    }

    #endregion

}
