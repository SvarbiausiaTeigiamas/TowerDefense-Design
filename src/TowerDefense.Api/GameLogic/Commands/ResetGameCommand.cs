using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.GameLogic.Commands;

public class ResetGameCommand : ICommand
{
    private readonly INotificationHub _notificationHub;
    private State _previousState;

    public ResetGameCommand(INotificationHub notificationHub)
    {
        _notificationHub = notificationHub;
    }

    public async Task Execute()
    {
        _previousState = GameOriginator.GameState;
        await _notificationHub.ResetGame();
        GameOriginator.GameState = new State();
    }

    public async Task Undo()
    {
        GameOriginator.GameState = _previousState;
        await _notificationHub.ResetGame();
    }
}
