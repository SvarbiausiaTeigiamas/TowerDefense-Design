using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class WaitingForPlayersState : IGameState
{
    private readonly GameContext _context;
    private readonly State _gameState;
    private readonly IInitialGameSetupHandler _setupHandler;

    public WaitingForPlayersState(GameContext context, State gameState, IInitialGameSetupHandler setupHandler)
    {
        _context = context;
        _gameState = gameState;
        _setupHandler = setupHandler;
    }

    public void AddPlayer(string playerName)
    {
        Console.WriteLine("WaitingForPlayersState: Adding new player...");
        _setupHandler.AddNewPlayer(playerName);
        Console.WriteLine($"Player '{playerName}' added. Active players: {_gameState.ActivePlayers}");

        if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
        {
            Console.WriteLine("Maximum players reached. Transitioning to InProgressState...");
            _context.SetState(_context.InProgressState);
        }
    }

    public async Task TryStartGame()
    {
        Console.WriteLine("WaitingForPlayersState: Attempting to start game...");
        if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
        {
            await _setupHandler.TryStartGame();
            Console.WriteLine("Game started. State should now be InProgressState.");
        }
        else
        {
            Console.WriteLine("Not enough players to start the game yet.");
        }
    }

    public Task EndTurn(string playerName)
    {
        Console.WriteLine("WaitingForPlayersState: EndTurn called but game not in progress yet.");
        return Task.CompletedTask;
    }

    public Task FinishGame(string winnerName)
    {
        Console.WriteLine("WaitingForPlayersState: FinishGame called but no game in progress.");
        return Task.CompletedTask;
    }
}