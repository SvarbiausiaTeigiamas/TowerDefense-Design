using TowerDefense.Api.GameLogic.Items;

namespace TowerDefense.Api.GameLogic.Strategies;

public interface IPricingStrategy
{
    int GetPrice(IItem item);
}
