namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class CommandRegistryTests
{
    private ICommandRegistry commandRegistry;

    [SetUp]
    public void SetUp()
    {
        this.commandRegistry = CommandRegistry.Instance;
    }

    [Test]
    public void RegisterAndGetCommand_ValidCommand_ShouldSucceed()
    {
        // Arrange
        var commandName = "MyCommand";
        var command = new RelayCommand(param => { });

        // Act
        this.commandRegistry.RegisterCommand(commandName, command);
        var retrievedCommand = this.commandRegistry.GetCommand(commandName);

        // Assert
        Assert.That(retrievedCommand, Is.SameAs(command));
    }

    [Test]
    public void GetCommand_NonExistentCommand_ShouldReturnNull()
    {
        // Arrange
        var commandName = "NonExistentCommand";

        // Act
        var retrievedCommand = this.commandRegistry.GetCommand(commandName);

        // Assert
        Assert.That(retrievedCommand, Is.Null);
    }

    [Test]
    public void RegisterCommand_OverwriteExistingCommand_ShouldUpdateCommand()
    {
        // Arrange
        var commandName = "MyCommand";
        var initialCommand = new RelayCommand(param => { });
        var updatedCommand = new RelayCommand(param => { });

        // Act
        this.commandRegistry.RegisterCommand(commandName, initialCommand);
        this.commandRegistry.RegisterCommand(commandName, updatedCommand);
        var retrievedCommand = this.commandRegistry.GetCommand(commandName);

        // Assert
        Assert.That(retrievedCommand, Is.SameAs(updatedCommand));
    }

    [Test]
    public void UnregisterCommand_ValidCommand_ShouldRemoveCommand()
    {
        // Arrange
        var commandName = "MyCommand";
        var command = new RelayCommand(param => { });
        this.commandRegistry.RegisterCommand(commandName, command);

        // Act
        this.commandRegistry.UnregisterCommand(commandName);
        var retrievedCommand = this.commandRegistry.GetCommand(commandName);

        // Assert
        Assert.That(retrievedCommand, Is.Null);
    }
}
