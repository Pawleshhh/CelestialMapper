using System.Windows.Input;

namespace CelestialMapper.ViewModel;

public class CommandRegistry : ICommandRegistry
{
    private static CommandRegistry? instance;
    private readonly Dictionary<string, ICommand> commands;

    public static CommandRegistry Instance => instance ??= new CommandRegistry();

    private CommandRegistry()
    {
        this.commands = new Dictionary<string, ICommand>();
    }

    public void RegisterCommand(string commandName, ICommand command)
    {
        if (!this.commands.ContainsKey(commandName))
        {
            this.commands.Add(commandName, command);
        }
        else
        {
            this.commands[commandName] = command;
        }
    }

    public ICommand? GetCommand(string commandName)
    {
        if (this.commands.TryGetValue(commandName, out var command))
        {

            return command;
        }

        return null;
    }

    public void UnregisterCommand(string commandName)
    {
        this.commands.Remove(commandName);
    }
}
