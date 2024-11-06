using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.GameLogic.Commands;

public class FinishGameCommand : ICommand
{
    private readonly INotificationHub _notificationHub;
    private readonly IPlayer _winnerPlayer;
    private State _previousState;

    public FinishGameCommand(INotificationHub notificationHub, IPlayer winnerPlayer)
    {
        _notificationHub = notificationHub;
        _winnerPlayer = winnerPlayer;
    }

    public async Task Execute()
    {
        _previousState = GameOriginator.GameState;
        await _notificationHub.NotifyGameFinished(_winnerPlayer);
        GameOriginator.GameState = new State();
    }

    public async Task Undo()
    {
        GameOriginator.GameState = _previousState;
        await _notificationHub.ResetGame();
    }
}
