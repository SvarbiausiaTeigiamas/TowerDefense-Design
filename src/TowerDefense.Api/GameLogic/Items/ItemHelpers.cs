using System.Numerics;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;
using Plane = TowerDefense.Api.GameLogic.Items.Models.Plane;

namespace TowerDefense.Api.GameLogic.Items
{
    public class ItemFactory
    {
        private static readonly Dictionary<ItemType, IItem> _itemCache = new();

        public static IItem GetItem(ItemType itemType)
        {
            if (!_itemCache.ContainsKey(itemType))
            {
                _itemCache[itemType] = CreateNewItem(itemType);
            }
            return _itemCache[itemType];
        }

        private static IItem CreateNewItem(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Blank => new Blank(),
                ItemType.Plane => new Plane(),
                ItemType.Rockets => new Rockets(),
                ItemType.Shield => new Shield(),
                ItemType.Placeholder => new Placeholder(),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }

    // Modified GridItem to work with flyweight pattern
    public class GridItem
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; }  // Store the type instead of the instance
        public IItem Item => ItemFactory.GetItem(ItemType);  // Get shared instance on demand

        // Add any grid-specific state here (position, health, etc.)
        public int Health { get; set; }
        public Vector2 Position { get; set; }
        // ... other extrinsic state
    }

    public static class ItemHelpers
    {
        public static IItem CreateItemByType(ItemType item)
        {
            return ItemFactory.GetItem(item);  // Use factory instead of direct creation
        }

        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<GridItem> GetAttackedRowItems(this IArenaGrid arenaGrid, int rowId)
        {
            return arenaGrid
                .GridItems.Where(x =>
                    (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId
                )
                .Select(x => new GridItem
                {
                    Id = x.Id,
                    ItemType = x.ItemType,
                    // Copy other necessary properties
                })
                .ToList();
        }

        public static IEnumerable<int> GetAttackedGridItems(
            IArenaGrid opponentsArenaGrid,
            int attackingGridItemId
        )
        {
            var attackerRowId = GetAttackingItemRowId(attackingGridItemId);
            var affectedGridItems = new List<int>();
            var row = opponentsArenaGrid.GetAttackedRowItems(attackerRowId);

            foreach (var gridItem in row)
            {
                if (!IsItemDamageable(gridItem))
                    continue;

                affectedGridItems.Add(gridItem.Id);
            }

            return affectedGridItems;
        }

        public static bool IsItemDamageable(GridItem gridItem)
        {
            if (gridItem == null)
                return false;

            var itemType = gridItem.ItemType;
            return itemType != ItemType.Blank && itemType != ItemType.Placeholder;
        }
    }
}
