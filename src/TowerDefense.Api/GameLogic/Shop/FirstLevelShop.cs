using TowerDefense.Api.GameLogic.Items;
using TowerDefense.Api.GameLogic.Items.Models;
using TowerDefense.Api.GameLogic.Strategies;

namespace TowerDefense.Api.GameLogic.Shop;

public class FirstLevelShop : IShop
{
    private readonly IPricingStrategy _pricingStrategy;
    private List<IItem> _items;

    public FirstLevelShop(IPricingStrategy pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
        _items = new List<IItem> { new Rockets(), new Shield(), new Plane() };
    }

    public IEnumerable<IItem> Items => _items;
}
