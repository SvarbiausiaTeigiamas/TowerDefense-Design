using System.Numerics;
using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Items
{
    public interface IFlyweightItem : IItem
    {
        new ItemType Type { get; }
        new int Id { get; }
    }

    public class ConcreteFlyweight : IFlyweightItem
    {
        string IItem.Id { get; set; }
        public int Id { get; }
        public ItemType Type { get; }
        public IItemStats Stats { get; set; }
        public ItemType ItemType { get; set; }

        public IEnumerable<AttackDeclaration> Attack(IArenaGrid opponentsArenaGrid, int attackingGridItemId)
        {
            return Enumerable.Empty<AttackDeclaration>();
        }

        public ConcreteFlyweight(ItemType type)
        {
            Type = type;
            ItemType = type;
        }
    }

    public class UnsharedConcreteFlyweight : IFlyweightItem
    {
        string IItem.Id { get; set; }
        public int Id { get; private set; }
        public ItemType Type { get; }
        public IItemStats Stats { get; set; }
        public ItemType ItemType { get; set; }

        internal int Health { get; set; }
        internal Vector2 Position { get; set; }

        public IEnumerable<AttackDeclaration> Attack(IArenaGrid opponentsArenaGrid, int attackingGridItemId)
        {
            return Enumerable.Empty<AttackDeclaration>();
        }

        public UnsharedConcreteFlyweight(ItemType type)
        {
            Type = type;
            ItemType = type;
        }

        internal void SetId(int id)
        {
            Id = id;
        }
    }

    public class ItemFactory
    {
        private static readonly Dictionary<ItemType, IFlyweightItem> _itemCache = new();

        public static IFlyweightItem GetItem(ItemType itemType, bool shared = true)
        {
            if (shared)
            {
                if (!_itemCache.ContainsKey(itemType))
                {
                    Console.WriteLine($"Creating new flyweight item of type: {itemType}");
                    _itemCache[itemType] = new ConcreteFlyweight(itemType);
                }
                else
                {
                    Console.WriteLine($"Reusing existing flyweight item of type: {itemType}");
                }
                return _itemCache[itemType];
            }

            return new UnsharedConcreteFlyweight(itemType);
        }
    }

    public class GridItem
    {
        internal readonly IFlyweightItem _item;

        public GridItem(ItemType itemType, bool shared = true)
        {
            _item = ItemFactory.GetItem(itemType, shared);
        }

        public ItemType ItemType => _item.ItemType;
        public int Id => _item.Id;
    }

    public static class GridItemExtensions
    {
        public static int GetHealth(this GridItem gridItem)
        {
            return gridItem._item is UnsharedConcreteFlyweight unshared ? unshared.Health : 0;
        }

        public static void SetHealth(this GridItem gridItem, int value)
        {
            if (gridItem._item is UnsharedConcreteFlyweight unshared)
            {
                unshared.Health = value;
            }
        }

        public static Vector2 GetPosition(this GridItem gridItem)
        {
            return gridItem._item is UnsharedConcreteFlyweight unshared ? unshared.Position : default;
        }

        public static void SetPosition(this GridItem gridItem, Vector2 value)
        {
            if (gridItem._item is UnsharedConcreteFlyweight unshared)
            {
                unshared.Position = value;
            }
        }
    }

    public static class ItemHelpers
    {
        public static IItem CreateItemByType(ItemType item, bool shared = true)
        {
            return ItemFactory.GetItem(item, shared);
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
                .Select(x =>
                {
                    var gridItem = new GridItem(x.ItemType, shared: false);
                    if (x.Id != 0 && gridItem._item is UnsharedConcreteFlyweight unshared)
                    {
                        unshared.SetId(x.Id);

                        // Since x is Grid.GridItem, we'll need to get these values directly
                        // or through a different mechanism that's available on Grid.GridItem
                        if (x is IGridItemProperties props)
                        {
                            unshared.Health = props.Health;
                            unshared.Position = props.Position;
                        }
                        // Alternative approach if the interface isn't available
                        else if (x.GetType().GetProperty("Health") != null && x.GetType().GetProperty("Position") != null)
                        {
                            unshared.Health = (int)x.GetType().GetProperty("Health").GetValue(x);
                            unshared.Position = (Vector2)x.GetType().GetProperty("Position").GetValue(x);
                        }
                    }
                    return gridItem;
                })
                .ToList();
        }

        // Interface that should be implemented by Grid.GridItem
        public interface IGridItemProperties
        {
            int Health { get; }
            Vector2 Position { get; }
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