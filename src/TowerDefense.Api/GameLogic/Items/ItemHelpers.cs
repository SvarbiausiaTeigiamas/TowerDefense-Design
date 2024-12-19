using System.Numerics;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;
using Plane = TowerDefense.Api.GameLogic.Items.Models.Plane;

namespace TowerDefense.Api.GameLogic.Items
{
    public class FlyweightFactory
    {
        private static readonly Dictionary<ItemType, IItem> _itemCache = new();

        public static IItem GetItem(ItemType itemType)
        {
            if (!_itemCache.ContainsKey(itemType))
            {
                Console.WriteLine($"Creating new flyweight item of type: {itemType}");
                _itemCache[itemType] = CreateNewItem(itemType);
            }
            else
            {
                Console.WriteLine($"Reusing existing flyweight item of type: {itemType}");
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
                ItemType.CompositeWeapon => new CompositeWeapon(),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }

    public class ExtrinsicFlyweight
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; } // Store the type instead of the instance
        public IItem Item => FlyweightFactory.GetItem(ItemType); // Get shared instance on demand

        public int Health { get; set; }
        public Vector2 Position { get; set; }
    }

    public static class ItemHelpers
    {
        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<ExtrinsicFlyweight> GetAttackedRowItems(
            this IArenaGrid arenaGrid,
            int rowId
        )
        {
            return arenaGrid.GridItems
                .Where(x => (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId)
                .Select(x => new ExtrinsicFlyweight { Id = x.Id, ItemType = x.ItemType })
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

        public static bool IsItemDamageable(ExtrinsicFlyweight gridItem)
        {
            if (gridItem == null)
                return false;

            var itemType = gridItem.ItemType;
            return itemType != ItemType.Blank && itemType != ItemType.CompositeWeapon;
        }
    }
}
