using System.Numerics;
using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;
using Plane = TowerDefense.Api.GameLogic.Items.Models.Plane;

namespace TowerDefense.Api.GameLogic.Items
{
    // This represents the intrinsic state
    public class ItemFlyweight : IItem
    {
        private readonly IItem _sharedItem;

        // Implement IItem properties
        public string Id
        {
            get => _sharedItem.Id;
            set => _sharedItem.Id = value;
        }

        public IItemStats Stats
        {
            get => _sharedItem.Stats;
            set => _sharedItem.Stats = value;
        }

        public ItemType ItemType
        {
            get => _sharedItem.ItemType;
            set => _sharedItem.ItemType = value;
        }

        public ItemFlyweight(ItemType itemType, IItem sharedItem)
        {
            _sharedItem = sharedItem;
        }

        // Implement IItem method
        public IEnumerable<AttackDeclaration> Attack(
            IArenaGrid opponentsArenaGrid,
            int attackingGridItemId)
        {
            return _sharedItem.Attack(opponentsArenaGrid, attackingGridItemId);
        }
    }

    // Factory for managing flyweight objects
    public class FlyweightFactory
    {
        private static readonly Dictionary<ItemType, ItemFlyweight> _flyweights = new();

        public static IItem GetFlyweight(ItemType itemType)
        {
            if (!_flyweights.ContainsKey(itemType))
            {
                Console.WriteLine($"Creating new flyweight for type: {itemType}");
                _flyweights[itemType] = new ItemFlyweight(itemType, CreateNewItem(itemType));
            }
            else
            {
                Console.WriteLine($"Reusing existing flyweight for type: {itemType}");
            }
            return _flyweights[itemType];  // Returns IItem since ItemFlyweight implements IItem
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

    // This represents the extrinsic state
    public class GridItemContext
    {
        public int Id { get; set; }
        private readonly IItem _item;
        public ItemType ItemType { get; }
        public IItem Item => _item;

        public int Health { get; set; }
        public Vector2 Position { get; set; }

        public GridItemContext(int id, ItemType itemType)
        {
            Id = id;
            ItemType = itemType;
            _item = FlyweightFactory.GetFlyweight(itemType);
        }
    }

    public static class ItemHelpers
    {
        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<GridItemContext> GetAttackedRowItems(this IArenaGrid arenaGrid, int rowId)
        {
            return arenaGrid
                .GridItems.Where(x =>
                    (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId
                )
                .Select(x => new GridItemContext(x.Id, x.ItemType))
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

        public static bool IsItemDamageable(GridItemContext gridItem)
        {
            if (gridItem == null)
                return false;

            var itemType = gridItem.ItemType;
            return itemType != ItemType.Blank && itemType != ItemType.Placeholder;
        }
    }
}