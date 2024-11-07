using TowerDefense.Api.GameLogic.Commands;
using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.Hubs;
using System.Collections.Generic;

namespace TowerDefense.Api.GameLogic.Handlers;

public interface IGameHandler
{
    Task ResetGame();
    Task FinishGame(IPlayer winnerPlayer);
}

class GameHandler : IGameHandler
{
    private readonly INotificationHub _notificationHub;

    public GameHandler(INotificationHub notificationHub)
    {
        _notificationHub = notificationHub;
    }

    public async Task ResetGame()
    {
        var resetGameCommand = new ResetGameCommand(_notificationHub);
        await resetGameCommand.Execute();
    }

    public async Task FinishGame(IPlayer winnerPlayer)
    {
        var finishGameCommand = new FinishGameCommand(_notificationHub, winnerPlayer);
        await finishGameCommand.Execute();
    }
}
