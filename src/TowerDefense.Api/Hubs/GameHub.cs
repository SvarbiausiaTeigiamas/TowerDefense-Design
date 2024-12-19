using Microsoft.AspNetCore.SignalR;
using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.Hubs
{
    public class GameHub : Hub
    {
        private readonly IInitialGameSetupHandler _initialGameSetup;
        private readonly GameContext _gameContext;

        public GameHub(IInitialGameSetupHandler initialGameSetup, GameContext gameContext)
        {
            _initialGameSetup = initialGameSetup;
            _gameContext = gameContext;
        }

        public async Task JoinGame(string playerName)
        {
            _initialGameSetup.SetConnectionIdForPlayer(playerName, Context.ConnectionId);
            await _initialGameSetup.TryStartGame();
            await _gameContext.TryStartGame();
        }
    }
}
