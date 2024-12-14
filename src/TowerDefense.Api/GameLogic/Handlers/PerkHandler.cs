using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Perks;
using TowerDefense.Api.GameLogic.PerkStorage;
using TowerDefense.Api.GameLogic.Player.Memento;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface IPerkHandler
    {
        IPerkStorage GetPerks(string playerName);
        void UsePerk(string perkUsingPlayerName, int perkId);
    }

    public class PerkHandler : IPerkHandler
    {
        private readonly State _gameState;
        private readonly ICareTaker _caretaker;

        public PerkHandler(ICareTaker caretaker)
        {
            _gameState = GameOriginator.GameState;
            _caretaker = caretaker;
        }

        public IPerkStorage GetPerks(string playerName)
        {
            var player = _gameState.Players.First(x => x.Name == playerName);

            return player.PerkStorage;
        }

        public void UsePerk(string perkUsingPlayerName, int perkId)
        {
            var player = _gameState.Players.First(x => x.Name == perkUsingPlayerName);
            var enemyPlayer = _gameState.Players.First(x => x.Name != perkUsingPlayerName);

            var perk = player.PerkStorage.Perks.FirstOrDefault(x => x.Id == perkId);

            if (perk == null)
                return;

            if (perk.Type == PerkType.ResetDamage)
            {
                var previousState = _caretaker.GetPreviousState();
                previousState.Restore();

                var newSnapshot = GameOriginator.SaveHealthSnapshot();
                _caretaker.AddSnapshot(newSnapshot);
                
                player.PerkStorage.Perks = player.PerkStorage.Perks.Where(x => x.Id != perkId); 
            }
        }
    }
}
