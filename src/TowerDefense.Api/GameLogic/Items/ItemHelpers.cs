using System.Numerics;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;
using Plane = TowerDefense.Api.GameLogic.Items.Models.Plane;

namespace TowerDefense.Api.GameLogic.Items
{
    // Flyweight interfaces and classes
    public interface IFlyweight
    {
        void Operation(ExtrinsicState state);
    }

    public class ExtrinsicState
    {
        public int GridPosition { get; set; }
    }

    public class IntrinsicState
    {
        public readonly int Health = 0;
        public readonly bool IsDestructible = false;
    }

    public class ConcreteFlyweight : IFlyweight
    {
        private readonly IntrinsicState _intrinsicState;

        public ConcreteFlyweight()
        {
            _intrinsicState = new IntrinsicState();
        }

        public void Operation(ExtrinsicState state)
        {
            // Implementation for shared operations
        }
    }

    public class FlyweightFactory
    {
        private static readonly FlyweightFactory _instance = new();
        private readonly Dictionary<string, IFlyweight> _flyweights = new();

        private FlyweightFactory() { }

        public static FlyweightFactory Instance => _instance;

        public IFlyweight GetFlyweight(string key)
        {
            if (!_flyweights.ContainsKey(key))
            {
                _flyweights[key] = new ConcreteFlyweight();
            }
            return _flyweights[key];
        }
    }

    // Modified GridItem to work with flyweight pattern
    public class Flyweight
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; } // Store the type instead of the instance
        public IItem Item => ItemFactory.GetItem(ItemType); // Get shared instance on demand

        public void SetGridPosition(int position)
        {
            _extrinsicState.GridPosition = position;
            _flyweight.Operation(_extrinsicState);
        }
    }

    // Helper class for item operations
    public static class ItemHelpers
    {
        

        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<Flyweight> GetAttackedRowItems(this IArenaGrid arenaGrid, int rowId)
        {
            return arenaGrid
                .GridItems.Where(x =>
                    (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId
                )
                .Select(x => new Flyweight
                {
                    Id = x.Id,
                    ItemType = x.ItemType,
                })
                .ToList();
        }

        public static IEnumerable<int> GetAttackedGridItems(Grid.IArenaGrid opponentsArenaGrid, int attackingGridItemId)
        {
            var attackerRowId = GetAttackingItemRowId(attackingGridItemId);
            var affectedGridItems = new List<int>();
            var row = opponentsArenaGrid.GetAttackedRowItems(attackerRowId);

            foreach (var gridItem in row)
            {
                if (!IsItemDamageable(gridItem)) continue;
                affectedGridItems.Add(gridItem.Id);
            }

            return affectedGridItems;
        }

        public static bool IsItemDamageable(Flyweight gridItem)
        {
            if (gridItem == null) return false;
            return gridItem.Item is not Blank && gridItem.Item is not Placeholder;
        }
    }
}


