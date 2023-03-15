namespace CelestialMapper.Common;

public static class ResourceHelper
{

    public static Stream GetResource<T>(string path)
    {
        var resource = typeof(T).Assembly.GetManifestResourceStream(path);
        return resource ?? Stream.Null;
    }

}
