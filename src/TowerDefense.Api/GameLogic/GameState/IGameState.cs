namespace TowerDefense.Api.GameLogic.GameState;

public interface IGameState
{
    void AddPlayer(string playerName);
    Task TryStartGame();
    Task EndTurn(string playerName);
    Task FinishGame(string winnerName);
}