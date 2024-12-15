namespace TowerDefense.Api.GameLogic.Interpreter;

public class CommandRegistry
{
    private readonly Dictionary<string, ICommand> _commands = new();

    public void RegisterCommand(string name, ICommand command)
    {
        _commands[name.ToLower()] = command;
    }

    public bool TryGetCommand(string name, out ICommand command)
    {
        return _commands.TryGetValue(name.ToLower(), out command);
    }

    public Dictionary<string, ICommand> GetCommands()
    {
        return _commands;
    }
}