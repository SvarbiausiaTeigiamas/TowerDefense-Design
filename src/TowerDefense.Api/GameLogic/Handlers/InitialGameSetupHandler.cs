using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Mediator;
using TowerDefense.Api.GameLogic.PerkStorage;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Player.Memento;
using TowerDefense.Api.GameLogic.Shop;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public class InitialGameSetupHandler : IInitialGameSetupHandler
    {
        private readonly State _gameState;
        private readonly INotificationHub _notificationHub;
        private readonly ICareTaker _caretaker;
        private readonly IMediator _mediator;
        private readonly PlayerSetupHandler _playerSetupChain;

        public InitialGameSetupHandler(INotificationHub notificationHub, ICareTaker caretaker, IMediator mediator)
        {
            _gameState = GameOriginator.GameState;
            _notificationHub = notificationHub;
            _caretaker = caretaker;
            _mediator = mediator;

            // Initialize the chain
            var addPlayerHandler = new AddPlayerHandler(_gameState, _mediator);
            var arenaGridHandler = new ArenaGridSetupHandler(_gameState);
            var shopHandler = new ShopSetupHandler(_gameState);
            var perkStorageHandler = new PerkStorageSetupHandler(_gameState);

            // Set up the chain
            addPlayerHandler
                .SetNext(arenaGridHandler)
                .SetNext(shopHandler)
                .SetNext(perkStorageHandler);

            _playerSetupChain = addPlayerHandler;
        }

        public IPlayer AddNewPlayer(string playerName)
        {
            var newPlayer = new FirstLevelPlayer { Name = playerName };
            return _playerSetupChain.Handle(newPlayer);
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

        public IPlayer AddPlayerToGame(string playerName)
        {
            var newPlayer = new FirstLevelPlayer { Name = playerName };
            return new AddPlayerHandler(_gameState, _mediator).Handle(newPlayer);
        }

        public void SetArenaGridForPlayer(IPlayer player)
        {
            new ArenaGridSetupHandler(_gameState).Handle(player);
        }

        public void SetShopForPlayer(IPlayer player)
        {
            new ShopSetupHandler(_gameState).Handle(player);
        }

        public void SetPerkStorageForPlayer(IPlayer player)
        {
            new PerkStorageSetupHandler(_gameState).Handle(player);
        }
    }
}
