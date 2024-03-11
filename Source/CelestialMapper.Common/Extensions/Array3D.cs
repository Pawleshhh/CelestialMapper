namespace CelestialMapper.Common;

public static class Array3D
{
    public static (int Rank0, int Rank1, int Rank2) GetLength<T>(this T[,,] array3d)
        => (array3d.GetLength(0), array3d.GetLength(1), array3d.GetLength(2));

    public static T[,,] Create<T>(int length1, int length2, int length3, T value)
    {
        T[,,] array3d = new T[length1, length2, length3];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    array3d[i, j, k] = value;
                }
            }
        }
        return array3d;
    }

    public static T[,,] CreateDefault<T>(int length1, int length2, int length3)
    {
        T[,,] array3d = new T[length1, length2, length3];
        return array3d;
    }

    public static T[,,] Initialize<T>(int length1, int length2, int length3, Func<int, int, int, T> initializer)
    {
        T[,,] array3d = new T[length1, length2, length3];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    array3d[i, j, k] = initializer(i, j, k);
                }
            }
        }
        return array3d;
    }

    public static void ForEach<T>(this T[,,] array3d, Action<T> action)
    {
        var (length1, length2, length3) = array3d.GetLength();
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    action(array3d[i, j, k]);
                }
            }
        }
    }

    public static void Iterate<T>(this T[,,] array3d, Action<int, int, int, T> action)
    {
        var (length1, length2, length3) = array3d.GetLength();
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    action(i, j, k, array3d[i, j, k]);
                }
            }
        }
    }

    public static U[,,] Select<T, U>(this T[,,] array3d, Func<T, U> map)
    {
        var (length1, length2, length3) = array3d.GetLength();
        U[,,] newArray3d = new U[length1, length2, length3];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    newArray3d[i, j, k] = map(array3d[i, j, k]);
                }
            }
        }
        return newArray3d;
    }

    public static U[,,] Select<T, U>(this T[,,] array3d, Func<int, int, int, T, U> map)
    {
        var (length1, length2, length3) = array3d.GetLength();
        U[,,] newArray3d = new U[length1, length2, length3];
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                for (int k = 0; k < length3; k++)
                {
                    newArray3d[i, j, k] = map(i, j, k, array3d[i, j, k]);
                }
            }
        }
        return newArray3d;
    }

}
