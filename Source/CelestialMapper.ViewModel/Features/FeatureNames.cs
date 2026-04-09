using System.Runtime.CompilerServices;

namespace CelestialMapper.ViewModel;

public record FeatureName
{

    public static FeatureName Unknown { get; } = new();

    public bool IsUnknown() => ReferenceEquals(this, Unknown);

    public string Name { get; init; }

    public string ViewName { get; }

    public FeatureName([CallerMemberName]string name = "")
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

    public static FeatureName Shell { get; } = new();

    public static FeatureName Map { get; } = new();

    public static FeatureName MapEditor { get; } = new();

    public static FeatureName MapEditorMenu { get; } = new();

    public static FeatureName MapEditorProperties { get; } = new();

    public static FeatureName TimeMachine { get; } = new();

    public static FeatureName ToolboxMenu { get; } = new();

    public static FeatureName PaperItemsCollection { get; } = new();

    public static FeatureName PropertiesMenu { get; } = new();

    public static FeatureName ExportMenu { get; } = new();

    public static FeatureName Paper { get; } = new();
    
    public static FeatureName PaperEditorMenu { get; } = new();

    public static FeatureName TextItem { get; } = new();

}
