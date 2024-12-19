using System.Threading.Tasks;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class GameContext
{
    private IGameState _currentState;

    public IGameState WaitingForPlayersState { get; }
    public IGameState InProgressState { get; }
    public IGameState GameFinishedState { get; }

    public GameContext(State gameState, ITurnHandler turnHandler, IGameHandler gameHandler)
    {
        Console.WriteLine("GameContext: Initializing state machine...");
        WaitingForPlayersState = new WaitingForPlayersState(this, gameState);
        InProgressState = new InProgressState(this, gameState, turnHandler, gameHandler);
        GameFinishedState = new GameFinishedState(gameHandler);

        _currentState = WaitingForPlayersState;
        Console.WriteLine("GameContext: Set initial state to WaitingForPlayersState");
    }

    public void SetState(IGameState newState)
    {
        Console.WriteLine($"GameContext: Transitioning to {newState.GetType().Name}");
        _currentState = newState;
    }

    public void AddPlayer(string playerName)
    {
        Console.WriteLine($"GameContext: Player '{playerName}' added to state machine.");
        _currentState.AddPlayer(playerName);
    }

    public Task TryStartGame()
    {
        Console.WriteLine("GameContext: Attempting to start game via state machine...");
        return _currentState.TryStartGame();
    }

    public Task EndTurn(string playerName)
    {
        Console.WriteLine($"GameContext: Player '{playerName}' ended their turn (requested).");
        return _currentState.EndTurn(playerName);
    }

    public Task FinishGame(string winnerName)
    {
        Console.WriteLine($"GameContext: Finishing game. Winner: {winnerName}");
        return _currentState.FinishGame(winnerName);
    }
}
