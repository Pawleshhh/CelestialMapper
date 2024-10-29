namespace CelestialMapper.ViewModel;

public record FeatureName
{

    public static FeatureName Unknown { get; } = new(nameof(Unknown));

    public bool IsUnknown() => ReferenceEquals(this, Unknown);

    public string Name { get; init; }

    public string ViewName { get; }

    public FeatureName(string name)
    {
        Name = name;
        ViewName = name + "View";
    }

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name.GetHashCode());
    }
}

public class FeatureNames
{

    public static FeatureName Shell { get; } = new(nameof(Shell));

    public static FeatureName Map { get; } = new(nameof(Map));

    public static FeatureName TimeMachine { get; } = new(nameof(TimeMachine));

    public static FeatureName Menu { get; } = new(nameof(Menu));

    public static FeatureName ExportMenu { get; } = new(nameof(ExportMenu));

    public static FeatureName Paper { get; } = new(nameof(Paper));
    
    public static FeatureName PaperEditorMenu { get; } = new(nameof(PaperEditorMenu));

    public static FeatureName TextItem { get; } = new(nameof(TextItem));

}
