using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class InProgressState : IGameState
{
    private readonly GameContext _context;
    private readonly State _gameState;
    private readonly ITurnHandler _turnHandler;
    private readonly IGameHandler _gameHandler;

    public InProgressState(GameContext context, State gameState, ITurnHandler turnHandler, IGameHandler gameHandler)
    {
        _context = context;
        _gameState = gameState;
        _turnHandler = turnHandler;
        _gameHandler = gameHandler;
    }

    public void AddPlayer(string playerName)
    {
        Console.WriteLine("InProgressState: Cannot add players, game already started.");
    }

    public Task TryStartGame()
    {
        Console.WriteLine("InProgressState: Game is already in progress.");
        return Task.CompletedTask;
    }

    public async Task EndTurn(string playerName)
    {
        Console.WriteLine($"InProgressState: {playerName} ended their turn.");
        await _turnHandler.TryEndTurn(playerName);

        // Check if game ended after turn resolution
        var player1 = _gameState.Players[0];
        var player2 = _gameState.Players[1];

        if (player1.Health <= 0 || player2.Health <= 0)
        {
            var winner = player1.Health > 0 ? player1.Name : player2.Name;
            await FinishGame(winner);
        }
    }

    public async Task FinishGame(string winnerName)
    {
        Console.WriteLine($"InProgressState: Game finished! Winner: {winnerName}");
        await _gameHandler.FinishGame(_gameState.Players.First(p => p.Name == winnerName));
        _context.SetState(_context.GameFinishedState);
    }
}