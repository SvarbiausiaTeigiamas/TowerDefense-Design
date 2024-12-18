using System.Numerics;
using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Items
{
    public interface IFlyweight : IItem
    {
        void Operation(ExtrinsicState state);
    }

    public class ExtrinsicState
    {
        public int Id { get; set; }
        public int Health { get; set; }
        public Vector2 Position { get; set; }
    }

    public abstract class BaseFlyweight : IFlyweight
    {
        protected readonly ItemType IntrinsicState;

        // Minimal IItem implementation just to satisfy interface
        public string Id { get; set; } = string.Empty;
        public IItemStats Stats { get; set; } = null;
        public ItemType ItemType { get; set; }

        public IEnumerable<AttackDeclaration> Attack(IArenaGrid opponentsArenaGrid, int attackingGridItemId)
        {
            return Enumerable.Empty<AttackDeclaration>();
        }

        protected BaseFlyweight(ItemType intrinsicState)
        {
            IntrinsicState = intrinsicState;
            ItemType = intrinsicState;  // Set the ItemType to match intrinsic state
        }

        public virtual void Operation(ExtrinsicState state)
        {
            Console.WriteLine($"Performing operation with item type {IntrinsicState}");
            Console.WriteLine($"Position: {state.Position}, Health: {state.Health}, Id: {state.Id}");
        }
    }

    // Rest of the concrete implementations remain the same
    public class BlankFlyweight : BaseFlyweight
    {
        public BlankFlyweight() : base(ItemType.Blank) { }
    }

    public class PlaneFlyweight : BaseFlyweight
    {
        public PlaneFlyweight() : base(ItemType.Plane) { }
    }

    public class RocketsFlyweight : BaseFlyweight
    {
        public RocketsFlyweight() : base(ItemType.Rockets) { }
    }

    public class ShieldFlyweight : BaseFlyweight
    {
        public ShieldFlyweight() : base(ItemType.Shield) { }
    }

    public class PlaceholderFlyweight : BaseFlyweight
    {
        public PlaceholderFlyweight() : base(ItemType.Placeholder) { }
    }

    // ItemFactory and the rest of the code remains exactly the same
    public class ItemFactory
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
                ItemType.Blank => new BlankFlyweight(),
                ItemType.Plane => new PlaneFlyweight(),
                ItemType.Rockets => new RocketsFlyweight(),
                ItemType.Shield => new ShieldFlyweight(),
                ItemType.Placeholder => new PlaceholderFlyweight(),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }

    public class GridItem
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; }
        public IItem Item => ItemFactory.GetItem(ItemType);
        public int Health { get; set; }
        public Vector2 Position { get; set; }

        public void PerformOperation()
        {
            if (Item is IFlyweight flyweight)
            {
                var state = new ExtrinsicState
                {
                    Id = Id,
                    Health = Health,
                    Position = Position
                };
                flyweight.Operation(state);
            }
        }
    }

    public static class ItemHelpers
    {
        public static IItem CreateItemByType(ItemType item)
        {
            return ItemFactory.GetItem(item);
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