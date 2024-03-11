namespace CelestialMapper.Common;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExportAttribute : Attribute
{

    public ExportAttribute(Type @interface, Type implementation)
        => (Interface, Implementation) = (@interface, implementation);

    public ExportAttribute(Type service)
        : this(service, service) { }

    public Type Interface { get; }

    public Type Implementation { get; }

    public required string Key { get; init; }

    public bool IsSingleton { get; init; }

}
