using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class GameFinishedState : IGameState
{
    private readonly IGameHandler _gameHandler;

    public GameFinishedState(IGameHandler gameHandler)
    {
        _gameHandler = gameHandler;
    }

    public void AddPlayer(string playerName)
    {
        Console.WriteLine("GameFinishedState: Can't add players, game ended. Consider resetting.");
    }

    public Task TryStartGame()
    {
        Console.WriteLine("GameFinishedState: Can't start game, already ended. Reset first.");
        return Task.CompletedTask;
    }

    public Task EndTurn(string playerName)
    {
        Console.WriteLine("GameFinishedState: Game ended. No turns to end.");
        return Task.CompletedTask;
    }

    public async Task FinishGame(string winnerName)
    {
        Console.WriteLine("GameFinishedState: FinishGame called again. Resetting game...");
        await _gameHandler.ResetGame();
    }
}