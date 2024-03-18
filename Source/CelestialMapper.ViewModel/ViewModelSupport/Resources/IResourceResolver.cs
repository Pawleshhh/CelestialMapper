namespace CelestialMapper.ViewModel;

public interface IResourceResolver
{

    public string ResolveString(string key);

    public bool TryResolveString(string key, out string value);

    public object ResolveResource(string key);

}
