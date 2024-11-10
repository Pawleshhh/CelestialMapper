using System.Reflection;

namespace CelestialMapper.Common;

[Export(typeof(IAttributeRetrievalService), typeof(AttributeRetrievalService), IsSingleton = true, IsKeyed = false, Key = nameof(AttributeRetrievalService))]
public class AttributeRetrievalService : IAttributeRetrievalService
{
    public IEnumerable<TAttribute> GetAttributes<TAttribute>(Assembly assembly, bool inherit = false) where TAttribute : Attribute
    {
        return assembly.GetTypes()
            .SelectMany(x => x.GetCustomAttributes(typeof(TAttribute), inherit))
            .Cast<TAttribute>();
    }
}
