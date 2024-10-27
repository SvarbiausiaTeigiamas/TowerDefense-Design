using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface ITurnHandler
    {   
        Task TryEndTurn(string playerName);
    }


    // Let's say, hypothetically, we want to create a copy of the TurnHandler - maybe for tests.
    // ICloneable interface allows us to do that - Prototype design pattern.
    // Couldn't find another place where this pattern could be more useful.
    public interface ICloneable<T>
    {
        T Clone();
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
            if (_gameState.PlayersFinishedTurn.ContainsKey(playerName)) return;
            _gameState.PlayersFinishedTurn.Add(playerName, true);

            if (_gameState.PlayersFinishedTurn.Count != Constants.TowerDefense.MaxNumberOfPlayers) return;

            await _battleHandler.HandleEndTurn();

            _gameState.PlayersFinishedTurn.Clear();
        }
    }
}
