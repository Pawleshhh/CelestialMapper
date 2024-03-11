namespace CelestialMapper.Common;

public static class Array2D
{

    public static (int RowLength, int ColumnLength) GetLength<T>(this T[,] array2d)
        => (array2d.GetLength(0), array2d.GetLength(1));

    public static T[,] Create<T>(int length1, int length2, T value)
    {
        T[,] array2d = new T[length1, length2];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                array2d[i, j] = value;
            }
        }
        return array2d;
    }

    public static T[,] CreateDefault<T>(int length1, int length2)
    {
        T[,] array2d = new T[length1, length2];
        return array2d;
    }

    public static T[,] Initialize<T>(int length1, int length2, Func<int, int, T> initializer)
    {
        T[,] array2d = new T[length1, length2];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                array2d[i, j] = initializer(i, j);
            }
        }
        return array2d;
    }

    public static void ForEach<T>(this T[,] array2d, Action<T> action)
    {
        var (length1, length2) = array2d.GetLength();
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                action(array2d[i, j]);
            }
        }
    }

    public static void Iterate<T>(this T[,] array2d, Action<int, int, T> action)
    {
        var (length1, length2) = array2d.GetLength();
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                action(i, j, array2d[i, j]);
            }
        }
    }

    public static U[,] Select<T, U>(this T[,] array2d, Func<T, U> map)
    {
        var (length1, length2) = array2d.GetLength();
        U[,] newArray2d = new U[length1, length2];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                newArray2d[i, j] = map(array2d[i, j]);
            }
        }
        return newArray2d;
    }


    public static U[,] Select<T, U>(this T[,] array2d, Func<int, int, T, U> map)
    {
        var (length1, length2) = array2d.GetLength();
        U[,] newArray2d = new U[length1, length2];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                newArray2d[i, j] = map(i, j, array2d[i, j]);
            }
        }
        return newArray2d;
    }

}
