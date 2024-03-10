namespace CelestialMapper.Common;

public static class ArrayExtensions
{

    public static T As<T>(this object[] array, int index)
        => (T)array[index];

}
