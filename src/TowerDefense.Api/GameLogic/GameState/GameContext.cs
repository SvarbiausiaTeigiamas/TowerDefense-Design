using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.GameLogic.GameState;

public class GameContext
{
    private IGameState _currentState;

    public IGameState WaitingForPlayersState { get; }
    public IGameState InProgressState { get; }
    public IGameState GameFinishedState { get; }

    public GameContext(State gameState, IInitialGameSetupHandler setupHandler, ITurnHandler turnHandler, IGameHandler gameHandler)
    {
        WaitingForPlayersState = new WaitingForPlayersState(this, gameState, setupHandler);
        InProgressState = new InProgressState(this, gameState, turnHandler, gameHandler);
        GameFinishedState = new GameFinishedState(gameHandler);

        _currentState = WaitingForPlayersState;
    }

    public void SetState(IGameState newState)
    {
        _currentState = newState;
    }

    public void AddPlayer(string playerName) => _currentState.AddPlayer(playerName);
    public Task TryStartGame() => _currentState.TryStartGame();
    public Task EndTurn(string playerName) => _currentState.EndTurn(playerName);
    public Task FinishGame(string winnerName) => _currentState.FinishGame(winnerName);
}