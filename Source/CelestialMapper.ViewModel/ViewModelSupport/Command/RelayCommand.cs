namespace CelestialMapper.ViewModel;

using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object?> execute;
    private readonly Func<object?, bool>? canExecute;

    public RelayCommand(Action<object?> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

#pragma warning disable CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used

    public bool CanExecute(object? parameter)
    {
        return this.canExecute == null || this.canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        this.execute(parameter);
    }
}
