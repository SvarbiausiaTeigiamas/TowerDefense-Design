namespace TowerDefense.Api.GameLogic.Interpreter;

public class HelpCommand: ICommand
{
    private readonly CommandRegistry _registry;
    
    public HelpCommand(CommandRegistry registry)
    {
        _registry = registry;
    }
    
    public string Description => "Shows list of available commands";

    public void Execute(string[] args = null)
    {
        Console.WriteLine("Available commands:");
        foreach (var cmd in _registry.GetCommands())
        {
            Console.WriteLine($"  {cmd.Key,-15} - {cmd.Value.Description}");
        }
    }
    
}