namespace TowerDefense.Api.Hubs;

using Contracts.Turn;
using GameLogic.Player;

public class NotificationHubProxy : INotificationHub
{
    private readonly Lazy<INotificationHub> _realHub;
    private readonly ILogger<NotificationHubProxy> _logger;

    public NotificationHubProxy(
        Func<INotificationHub> hubFactory,
        ILogger<NotificationHubProxy> logger
    )
    {
        _realHub = new Lazy<INotificationHub>(hubFactory);
        _logger = logger;
    }

    public async Task SendPlayersTurnResult(Dictionary<string, EndTurnResponse> responses)
    {
        try
        {
            _logger.LogInformation(
                "Sending turn results to {PlayerCount} players",
                responses.Count
            );
            await _realHub.Value.SendPlayersTurnResult(responses);
            _logger.LogInformation("Turn results sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send turn results");
            throw;
        }
    }

    public async Task NotifyGameStart(IPlayer firstPlayer, IPlayer secondPlayer)
    {
        try
        {
            _logger.LogInformation(
                "Starting game between {Player1} and {Player2}",
                firstPlayer.Name,
                secondPlayer.Name
            );
            await _realHub.Value.NotifyGameStart(firstPlayer, secondPlayer);
            _logger.LogInformation("Game start notification sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to notify game start");
            throw;
        }
    }

    public async Task ResetGame()
    {
        try
        {
            _logger.LogInformation("Resetting game");
            await _realHub.Value.ResetGame();
            _logger.LogInformation("Game reset notification sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset game");
            throw;
        }
    }

    public async Task NotifyGameFinished(IPlayer winner)
    {
        try
        {
            _logger.LogInformation("Game finished. Winner: {Winner}", winner.Name);
            await _realHub.Value.NotifyGameFinished(winner);
            _logger.LogInformation("Game finished notification sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to notify game finished");
            throw;
        }
    }
}
