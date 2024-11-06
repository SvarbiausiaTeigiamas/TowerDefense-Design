using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface ITurnHandler
    {
        Task TryEndTurn(string playerName);
    }

    public class TurnHandler : ITurnHandler, ICloneable<TurnHandler>
    {
        private readonly State _gameState;
        private readonly IBattleHandler _battleHandler;

        public TurnHandler(IBattleHandler battleHandler)
        {
            _gameState = GameOriginator.GameState;
            _battleHandler = battleHandler;
        }

        public TurnHandler Clone()
        {
            return new TurnHandler(this._battleHandler);
        }

        public async Task TryEndTurn(string playerName)
        {
            if (!_gameState.PlayersFinishedTurn.TryAdd(playerName, true))
                return;

            if (_gameState.PlayersFinishedTurn.Count != Constants.TowerDefense.MaxNumberOfPlayers)
                return;

            await _battleHandler.HandleEndTurn();

            _gameState.PlayersFinishedTurn.Clear();
        }
    }
}
