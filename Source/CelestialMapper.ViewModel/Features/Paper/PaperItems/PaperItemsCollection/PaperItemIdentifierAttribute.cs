namespace CelestialMapper.ViewModel;

[AttributeUsage(AttributeTargets.Class,  AllowMultiple = false, Inherited = true)]
public class PaperItemIdentifierAttribute : Attribute
{

    public required PaperItemCatergory Category { get; init; }

    public PaperItemType ItemType { get; init; } = PaperItemType.Unknown;

    public required string NameKey { get; init; }

}
