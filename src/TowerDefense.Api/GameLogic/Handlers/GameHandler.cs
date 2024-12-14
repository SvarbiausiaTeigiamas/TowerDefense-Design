using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Player.Memento;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface IGameHandler
    {
        Task ResetGame();
        Task FinishGame(IPlayer winnerPlayer);
    }

    class GameHandler : IGameHandler
    {
        private readonly INotificationHub _notificationHub;
        private readonly ICareTaker _caretaker;

        public GameHandler(INotificationHub notificationHub, ICareTaker caretaker)
        {
            _caretaker = caretaker;
            _notificationHub = notificationHub;
        }

        public async Task ResetGame()
        {
            await _notificationHub.ResetGame();
            GameOriginator.GameState = new State();
        }

        public async Task FinishGame(IPlayer winnerPlayer)
        {
            await _notificationHub.NotifyGameFinished(winnerPlayer);
            _caretaker.Clear();
            GameOriginator.GameState = new State();
        }
    }
}
