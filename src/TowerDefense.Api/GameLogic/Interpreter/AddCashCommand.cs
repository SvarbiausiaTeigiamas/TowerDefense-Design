using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class AddCashCommand: ICommand
{
    public string Description => "Adds specified amount of cash to both players";
        
    public void Execute(string[] args = null)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Please specify an amount");
            return;
        }

        if (int.TryParse(args[0], out var amount))
        {
            foreach (var player in GameOriginator.GameState.Players)
            {
                player.Money += int.Parse(args[0]);
            }
            
            Console.WriteLine($"Added {amount} cash to both players");
        }
        else
        {
            Console.WriteLine("Invalid amount specified");
        }
    }
}