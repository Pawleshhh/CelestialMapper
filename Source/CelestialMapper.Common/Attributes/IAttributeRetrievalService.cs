using System.Reflection;

namespace CelestialMapper.Common;

public interface IAttributeRetrievalService
{
    IEnumerable<TAttribute> GetAttributes<TAttribute>(Assembly assembly, bool inherit = false) where TAttribute : Attribute;
}
