using System.Windows.Input;

namespace CelestialMapper.ViewModel;

public class UICommand : UICommand<object>
{
    public UICommand(Action<object?> execute) : base(execute)
    {
    }

    public UICommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : base(execute, canExecute)
    {
    }
}

public class UICommand<T> : NotifyPropertyChangedBase, ICommand
{

    private readonly RelayCommand<T> relayCommand;

    public UICommand(Action<T?> execute)
    {
        this.relayCommand = new(execute);
    }

    public UICommand(Action<T?> execute, Func<T?, bool>? canExecute = null)
    {
        this.relayCommand = new(execute, canExecute);
    }

    public string Id { get; init; } = string.Empty;

    public string Label
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        set => SetPropertyValue(value);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => this.relayCommand.CanExecuteChanged += value;
        remove => this.relayCommand.CanExecuteChanged -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return this.relayCommand.CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        this.relayCommand.Execute(parameter);
    }
}
