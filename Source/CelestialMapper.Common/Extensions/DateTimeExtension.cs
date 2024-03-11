namespace CelestialMapper.Common;

public static class DateTimeExtension
{

    #region Deconstructors

    public static void Deconstruct(this DateTime dateTime, out int year, out int month, out int day)
    {
        year = dateTime.Year;
        month = dateTime.Month;
        day = dateTime.Day;
    }

    public static void Deconstruct(this DateTime dateTime, out int year, out int month, out int day, out int hour, out int minute, out int second)
    {
        Deconstruct(dateTime, out year, out month, out day);
        TimeSpanExtension.Deconstruct(dateTime.TimeOfDay, out hour, out minute, out second);
    }

    public static void Deconstruct(this DateTime dateTime, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    {
        Deconstruct(dateTime, out year, out month, out day);
        TimeSpanExtension.Deconstruct(dateTime.TimeOfDay, out hour, out minute, out second, out millisecond);
    }

    public static void Deconstruct(this DateTime dateTime, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond, out int microsecond)
    {
        Deconstruct(dateTime, out year, out month, out day);
        TimeSpanExtension.Deconstruct(dateTime.TimeOfDay, out hour, out minute, out second, out millisecond, out microsecond);
    }

    public static void Deconstruct(this DateTime dateTime, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond, out int microsecond, out int nanosecond)
    {
        Deconstruct(dateTime, out year, out month, out day);
        TimeSpanExtension.Deconstruct(dateTime.TimeOfDay, out hour, out minute, out second, out millisecond, out microsecond, out nanosecond);
    }

    #endregion

}
