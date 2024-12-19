using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class InProgressState : IGameState
{
    private readonly GameContext _context;
    private readonly State _gameState;
    private readonly ITurnHandler _turnHandler;
    private readonly IGameHandler _gameHandler;

    public InProgressState(
        GameContext context,
        State gameState,
        ITurnHandler turnHandler,
        IGameHandler gameHandler
    )
    {
        Console.WriteLine("InProgressState: Initialized");
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
        Console.WriteLine($"InProgressState: EndTurn called by {playerName}");
        await _turnHandler.TryEndTurn(playerName);
    }

    public async Task FinishGame(string winnerName)
    {
        Console.WriteLine($"InProgressState: FinishGame called. Winner: {winnerName}");
        var winner = _gameState.Players.First(p => p.Name == winnerName);
        await _gameHandler.FinishGame(winner);
        _context.SetState(_context.GameFinishedState);
    }
}
