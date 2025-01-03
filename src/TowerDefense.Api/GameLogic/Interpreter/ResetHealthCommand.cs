using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class ResetHealthCommand : ICommand
{
    public string Description => "Resets both players health and armor";

    public void Interpret(State state)
    {
        foreach (var player in state.Players)
        {
            player.Health = 100;
            player.Armor = 100;
        }
        Console.WriteLine("Reset both players health and armor to 100");
    }
}
