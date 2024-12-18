using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class AddCashCommand : ICommand
{
    private readonly int _amount;

    public AddCashCommand(int amount)
    {
        _amount = amount;
    }

    public void Interpret(State state)
    {
        foreach (var player in state.Players)
        {
            player.Money += _amount;
        }

        Console.WriteLine($"Added {_amount} cash to all players");
    }
}
