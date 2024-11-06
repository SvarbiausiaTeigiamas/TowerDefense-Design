using TowerDefense.Api.GameLogic.Items;

namespace TowerDefense.Api.GameLogic.Strategies;

public class PremiumPricingStrategy : IPricingStrategy
{
    private readonly int _premiumPercentage;

    public PremiumPricingStrategy(int premiumPercentage)
    {
        _premiumPercentage = premiumPercentage;
    }

    public int GetPrice(IItem item)
    {
        return item.Stats.Price * (100 + _premiumPercentage) / 100;
    }
}
