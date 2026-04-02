using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public class ChoicePropertyWrapper<T> : PropertyWrapper<T>
{

    public ObservableCollection<T> Choices { get; } = new();

    public ChoicePropertyWrapper()
        : base()
    {
        Initialize(Enumerable.Empty<T>(), default);
    }

    public ChoicePropertyWrapper(string? name)
        : base(name)
    {

        Initialize(Enumerable.Empty<T>(), default);
    }

    public ChoicePropertyWrapper(string? name, params T[] choices)
        : base(name)
    {

        Initialize(choices, default);
    }

    public ChoicePropertyWrapper(T value, string name)
        : base(value, name)
    {
        Initialize(Enumerable.Empty<T>(), value);
    }

    public ChoicePropertyWrapper(T value, string name, params T[] choices)
        : base(value, name)
    {
        Initialize(choices, value);
    }

    private void Initialize(IEnumerable<T> choices, T? value)
    {
        Choices.AddRange(choices);

        if (value is not null)
        {
            Value = value;
        }
        else
        {
            Value = Choices.FirstOrDefault();
        }
    }

}
