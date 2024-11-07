using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Items
{
    public static class ItemHelpers
    {
        private static readonly Dictionary<ItemType, IItemCreator> ItemCreators = new()
        {
            { ItemType.Blank, new BlankItemCreator() },
            { ItemType.Plane, new PlaneItemCreator() },
            { ItemType.Rockets, new RocketsItemCreator() },
            { ItemType.Shield, new ShieldItemCreator() },
            { ItemType.Placeholder, new PlaceholderItemCreator() }
        };

        public static IItem CreateItemByType(ItemType item)
        {
            if (ItemCreators.TryGetValue(item, out var creator))
            {
                return creator.CreateItem();
            }

            throw new ArgumentOutOfRangeException(nameof(item), item, null);
        }
        public static int GetAttackingItemRowId(int attackingGridItemId)
        {
            return attackingGridItemId / Constants.TowerDefense.MaxGridGridItemsInRow;
        }

        public static List<GridItem> GetAttackedRowItems(this IArenaGrid arenaGrid, int rowId)
        {
            return arenaGrid.GridItems
                .Where(x => (int)(x.Id / Constants.TowerDefense.MaxGridGridItemsInRow) == rowId)
                .ToList();
        }

        public static IEnumerable<int> GetAttackedGridItems(IArenaGrid opponentsArenaGrid, int attackingGridItemId)
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
        public static bool IsItemDamageable(GridItem gridItem)
        {
            if (gridItem == null) return false;
            return gridItem.Item is not Blank && gridItem.Item is not Placeholder;
        }
    }
}
