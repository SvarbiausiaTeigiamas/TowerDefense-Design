using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class HelpCommand : ICommand
{
    private static readonly Dictionary<string, string> CommandHelp =
        new()
        {
            ["add-cash"] = "Adds specified amount of cash to all players. Usage: add-cash <amount>",
            ["reset-health"] = "Resets health and armor for all players",
            ["help"] = "Shows list of available commands and their descriptions",
            ["*Command Sequencing*"] =
                "Execute multiple commands in sequence. Usage: command1 ; command2",
        };

    public void Interpret(State state)
    {
        Console.WriteLine("Available commands:");
        foreach (var (command, description) in CommandHelp)
        {
            Console.WriteLine($"  {command, -15} - {description}");
        }
    }
}
