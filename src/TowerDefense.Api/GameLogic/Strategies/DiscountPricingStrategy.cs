using TowerDefense.Api.GameLogic.Items;

namespace TowerDefense.Api.GameLogic.Strategies;

public class DiscountPricingStrategy : IPricingStrategy
{
    private readonly int _discountPercentage;

    public DiscountPricingStrategy(int discountPercentage)
    {
        _discountPercentage = discountPercentage;
    }

    public int GetPrice(IItem item)
    {
        return item.Stats.Price * (100 - _discountPercentage) / 100;
    }
}
