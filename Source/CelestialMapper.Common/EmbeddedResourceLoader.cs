using System.Reflection;

namespace CelestialMapper.Common;

public static class EmbeddedResourceLoader
{

    public static Stream GetResourceStream(string resourceName)
    {
        string fullResourceName = "CelestialMapper." + resourceName;

        Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName);

        if (stream is null)
        {
            throw new InvalidOperationException($"Resource '{fullResourceName}' not found.");
        }

        return stream;
    }

}
