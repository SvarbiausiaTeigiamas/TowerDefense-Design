using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Shop;
using TowerDefense.Api.GameLogic.Strategies;

namespace TowerDefense.Api.GameLogic.Factories;

public class AdvancedLevelGameFactory : IGameFactory
{
    public IShop CreateShop()
    {
        var pricingStrategy = new PremiumPricingStrategy(25);
        return new AdvancedLevelShop(pricingStrategy);
    }

    public IGridLayout CreateGridLayout()
    {
        return new JsonLayoutAdapter("GameLogic/Grid/advancedLayout.json");
    }

    public IPlayer CreatePlayer(string name)
    {
        return new AdvancedLevelPlayer(name);
    }
}
