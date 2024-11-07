using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Shop;
using TowerDefense.Api.GameLogic.Strategies;

namespace TowerDefense.Api.GameLogic.Factories;

public class FirstLevelGameFactory : IGameFactory
{
    public IShop CreateShop()
    {
        var pricingStrategy = new DiscountPricingStrategy(10);
        return new FirstLevelShop(pricingStrategy);
    }

    public IGridLayout CreateGridLayout()
    {
        return new ConstantLayoutAdapter();
    }

    public IPlayer CreatePlayer(string name)
    {
        return new FirstLevelPlayer(name);
    }
}


