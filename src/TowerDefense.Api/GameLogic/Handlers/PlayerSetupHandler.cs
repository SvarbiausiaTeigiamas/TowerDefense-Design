using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public abstract class PlayerSetupHandler
    {
        protected PlayerSetupHandler? _nextHandler;
        protected readonly State _gameState;

        protected PlayerSetupHandler(State gameState)
        {
            _gameState = gameState;
        }

        public PlayerSetupHandler SetNext(PlayerSetupHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual IPlayer Handle(IPlayer player)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(player);
            }
            Console.WriteLine("Chain of Responsibility: Setup chain completed");
            return player;
        }
    }
}
