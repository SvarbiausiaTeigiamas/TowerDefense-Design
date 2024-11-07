using TowerDefense.Api.GameLogic.Items;

namespace TowerDefense.Api.GameLogic.Strategies;

public class DynamicPricingStrategy : IPricingStrategy
{
    public int GetPrice(IItem item)
    {
        return item.Stats.Health * 2 + item.Stats.Damage * 3;
    }
}
