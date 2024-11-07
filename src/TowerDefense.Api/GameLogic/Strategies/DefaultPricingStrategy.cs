using TowerDefense.Api.GameLogic.Items;

namespace TowerDefense.Api.GameLogic.Strategies;

public class DefaultPricingStrategy : IPricingStrategy
{
    public int GetPrice(IItem item)
    {
        return item.Stats.Price;
    }
}
