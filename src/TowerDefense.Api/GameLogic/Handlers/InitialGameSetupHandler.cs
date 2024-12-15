using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.PerkStorage;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Player.Memento;
using TowerDefense.Api.GameLogic.Shop;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface IInitialGameSetupHandler
    {
        IPlayer AddNewPlayer(string playerName);
        void SetConnectionIdForPlayer(string playerName, string connectionId);
        IPlayer AddPlayerToGame(string playerName);
        void SetArenaGridForPlayer(IPlayer player);
        void SetShopForPlayer(IPlayer player);
        void SetPerkStorageForPlayer(IPlayer player);
        Task TryStartGame();
    }

    public class InitialGameSetupHandler : IInitialGameSetupHandler
    {
        private readonly State _gameState;
        private readonly INotificationHub _notificationHub;
        private readonly ICareTaker _caretaker;

        public InitialGameSetupHandler(INotificationHub notificationHub, ICareTaker caretaker)
        {
            _gameState = GameOriginator.GameState;
            _notificationHub = notificationHub;
            _caretaker = caretaker;
        }

        public IPlayer AddNewPlayer(string playerName)
        {
            var player = AddPlayerToGame(playerName);
            SetArenaGridForPlayer(player);
            SetShopForPlayer(player);
            SetPerkStorageForPlayer(player);
            return player;
        }

        public void SetConnectionIdForPlayer(string playerName, string connectionId)
        {
            var player = _gameState.Players.First(x => x.Name == playerName);
            player.ConnectionId = connectionId;
        }

        public async Task TryStartGame()
        {
            if (_gameState.ActivePlayers != Constants.TowerDefense.MaxNumberOfPlayers)
                return;

            var snapshot = GameOriginator.SaveHealthSnapshot();
            // save multiple times to avoid accessing empty stack if used too early
            _caretaker.AddSnapshot(snapshot);
            _caretaker.AddSnapshot(snapshot);
            _caretaker.AddSnapshot(snapshot);
            _caretaker.AddSnapshot(snapshot);

            await _notificationHub.NotifyGameStart(_gameState.Players[0], _gameState.Players[1]);
        }

        public void SetArenaGridForPlayer(IPlayer player)
        {
            string file = Path.GetFullPath("./GameLogic/Grid/CSVfile.csv");
            var arenaGrid = new FirstLevelArenaGrid(file);
            Console.WriteLine("Using TEMPLATE pattern, reading from " + file);
            player.ArenaGrid = arenaGrid;
        }

        public void SetShopForPlayer(IPlayer player)
        {
            var shop = new FirstLevelShop();
            player.Shop = shop;
        }

        public void SetPerkStorageForPlayer(IPlayer player)
        {
            var perkStorage = new FirstLevelPerkStorage();
            player.PerkStorage = perkStorage;
        }

        public IPlayer AddPlayerToGame(string playerName)
        {
            if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
            {
                throw new ArgumentException();
            }

            var currentNewPlayerId = _gameState.ActivePlayers;
            var newPlayer = new FirstLevelPlayer { Name = playerName };
            _gameState.Players[currentNewPlayerId] = newPlayer;

            return newPlayer;
        }
    }
}
