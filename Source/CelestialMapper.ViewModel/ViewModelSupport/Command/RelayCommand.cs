namespace CelestialMapper.ViewModel;

using System.Windows.Input;

public class RelayCommand : RelayCommand<object>
{
    public RelayCommand(Action<object?> execute) : base(execute)
    {
    }

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : base(execute, canExecute)
    {
    }
}

public class RelayCommand<T> : ICommand
{
    private readonly Action<T?> execute;
    private readonly Func<T?, bool>? canExecute;

    public RelayCommand(Action<T?> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

#pragma warning disable CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used

    public bool CanExecute(object? parameter)
    {
        return this.canExecute == null || this.canExecute((T?)parameter);
    }

    public void Execute(object? parameter)
    {
        this.execute((T?)parameter);
    }
}
