using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Mediator;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public class AddPlayerHandler : PlayerSetupHandler
    {
        private readonly IMediator _mediator;

        public AddPlayerHandler(State gameState, IMediator mediator)
            : base(gameState)
        {
            _mediator = mediator;
        }

        public override IPlayer Handle(IPlayer player)
        {
            if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
            {
                throw new ArgumentException();
            }

            var currentNewPlayerId = _gameState.ActivePlayers;
            _gameState.Players[currentNewPlayerId] = player;
            Console.WriteLine($"Chain of Responsibility: Player {player.Name} added to game state");

            _mediator.Notify(player, "PlayerAdded");

            return base.Handle(player);
        }
    }
}
