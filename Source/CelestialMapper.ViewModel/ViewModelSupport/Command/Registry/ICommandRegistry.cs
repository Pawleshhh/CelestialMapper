using System.Windows.Input;

namespace CelestialMapper.ViewModel;

public interface ICommandRegistry
{
    void RegisterCommand(string commandName, ICommand command);
    ICommand? GetCommand(string commandName);
    void UnregisterCommand(string commandName);
}
