namespace CelestialMapper.UI;

[Export(typeof(IResourceResolver), typeof(ResourceResolver), IsSingleton = true, Key = nameof(ResourceResolver))]
public class ResourceResolver : IResourceResolver
{
    public string ResolveString(string key)
    {
        var resource = App.Current.Resources[key] as string;

        if (resource is null)
        {
            throw new InvalidOperationException($"Resource of {key} not found");
        }

        return resource;
    }

    public bool TryResolveString(string key, out string value)
    {
        try
        {
            value = ResolveString(key);
            return true;
        }
        catch (InvalidOperationException)
        {
            value = key;
            return false;
        }
    }

}
