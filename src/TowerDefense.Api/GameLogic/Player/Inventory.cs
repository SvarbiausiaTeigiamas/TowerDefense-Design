using TowerDefense.Api.GameLogic.Items;
using System.Collections.Generic;
using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Player;

public class Inventory : CompositeItem
{
    public Inventory()
    {
        Id = "Inventory";
        ItemType = ItemType.Blank;
    }
}
