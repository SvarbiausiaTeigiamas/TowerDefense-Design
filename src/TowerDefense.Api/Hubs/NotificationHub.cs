using Microsoft.AspNetCore.SignalR;
using TowerDefense.Api.Contracts.Turn;
using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.GameLogic.Observers;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.Hubs;

public class NotificationHub : INotificationHub, IGameObserver
{
    private readonly IHubContext<GameHub> _gameHubContext;
    private readonly IPlayerHandler _playerHandler;

    public NotificationHub(IHubContext<GameHub> gameHubContext, IPlayerHandler playerHandler)
    {
        _playerHandler = playerHandler;
        _gameHubContext = gameHubContext;
        GameOriginator.Attach(this); // Attach to the subject
    }

    public async Task SendPlayersTurnResult(Dictionary<string, EndTurnResponse> responses)
    {
        foreach (var response in responses)
        {
            var player = _playerHandler.GetPlayer(response.Key);
            SendEndTurnInfo(player, response.Value);
        }
    }

    public async Task NotifyGameStart(IPlayer firstPlayer, IPlayer secondPlayer)
    {
        await _gameHubContext.Clients
            .Client(firstPlayer.ConnectionId)
            .SendAsync("EnemyInfo", secondPlayer);
        await _gameHubContext.Clients
            .Client(secondPlayer.ConnectionId)
            .SendAsync("EnemyInfo", firstPlayer);
    }

    public async Task SendEndTurnInfo(IPlayer player, EndTurnResponse turnOutcome)
    {
        await _gameHubContext.Clients.Client(player.ConnectionId).SendAsync("EndTurn", turnOutcome);
    }

    public async Task ResetGame()
    {
        var players = _playerHandler.GetPlayers().Where(x => x != null);
        foreach (var player in players)
        {
            await _gameHubContext.Clients.Client(player.ConnectionId).SendAsync("ResetGame");
        }
    }

    public async Task NotifyGameFinished(IPlayer winner)
    {
        var players = _playerHandler.GetPlayers().Where(x => x != null);
        foreach (var player in players)
        {
            await _gameHubContext.Clients
                .Client(player.ConnectionId)
                .SendAsync("GameFinished", winner.Name);
        }
    }

    public void Update(State gameState)
    {
        // Notify clients about the game state change
        _ = GameStateChanged(gameState);
    }

    private async Task GameStateChanged(State gameState)
    {
        foreach (var player in _playerHandler.GetPlayers().Where(x => x != null))
        {
            await _gameHubContext.Clients
                .Client(player.ConnectionId)
                .SendAsync("GameStateChanged", gameState);
        }
    }
}
