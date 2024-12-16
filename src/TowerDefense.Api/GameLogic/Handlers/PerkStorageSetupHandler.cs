using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.PerkStorage;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public class PerkStorageSetupHandler : PlayerSetupHandler
    {
        public PerkStorageSetupHandler(State gameState)
            : base(gameState) { }

        public override IPlayer Handle(IPlayer player)
        {
            var perkStorage = new FirstLevelPerkStorage();
            player.PerkStorage = perkStorage;
            Console.WriteLine(
                $"Chain of Responsibility: Perk storage created for player {player.Name}"
            );

            return base.Handle(player);
        }
    }
}
