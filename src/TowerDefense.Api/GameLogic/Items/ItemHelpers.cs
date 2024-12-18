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

    // Concrete item implementations
    public class Blank
    {
        private readonly IFlyweight _flyweight;
        private readonly ExtrinsicState _extrinsicState;

        public Blank()
        {
            _flyweight = FlyweightFactory.Instance.GetFlyweight("Blank");
            _extrinsicState = new ExtrinsicState();
        }

        public bool IsDestructible => false;
        public int Health => 0;

        public void SetGridPosition(int position)
        {
            _extrinsicState.GridPosition = position;
            _flyweight.Operation(_extrinsicState);
        }
    }

    // Helper class for item operations
    public static class ItemHelpers
    {
        public static IItem CreateItemByType(ItemType item)
        {
            return item switch
            {
             
                ItemType.Plane => new Plane(),
                ItemType.Rockets => new Rockets(),
                ItemType.Shield => new Shield(),
                ItemType.Placeholder => new Placeholder(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<Grid.GridItem> GetAttackedRowItems(this Grid.IArenaGrid arenaGrid, int rowId)
        {
            return arenaGrid.GridItems
                .Where(x => (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId)
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

        public static bool IsItemDamageable(Grid.GridItem gridItem)
        {
            if (gridItem == null) return false;
            return gridItem.Item is not Blank && gridItem.Item is not Placeholder;
        }
    }
}


