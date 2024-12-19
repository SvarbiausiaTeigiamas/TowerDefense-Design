using System;
using System.Threading.Tasks;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class GameFinishedState : IGameState
{
    private readonly IGameHandler _gameHandler;

    public GameFinishedState(IGameHandler gameHandler)
    {
        Console.WriteLine("GameFinishedState: Initialized");
        _gameHandler = gameHandler;
    }

    public void AddPlayer(string playerName)
    {
        Console.WriteLine("GameFinishedState: AddPlayer called but game is finished.");
    }

    public Task TryStartGame()
    {
        Console.WriteLine("GameFinishedState: TryStartGame called but game is finished.");
        return Task.CompletedTask;
    }

    public Task EndTurn(string playerName)
    {
        Console.WriteLine("GameFinishedState: EndTurn called but game is finished.");
        return Task.CompletedTask;
    }

    public async Task FinishGame(string winnerName)
    {
        Console.WriteLine("GameFinishedState: FinishGame called again. Resetting game...");
        await _gameHandler.ResetGame();
    }
}
