using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Shop;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public class ShopSetupHandler : PlayerSetupHandler
    {
        public ShopSetupHandler(State gameState)
            : base(gameState) { }

        public override IPlayer Handle(IPlayer player)
        {
            var shop = new FirstLevelShop();
            player.Shop = shop;
            Console.WriteLine(
                $"Chain of Responsibility: Shop initialized for player {player.Name}"
            );

            return base.Handle(player);
        }
    }
}
