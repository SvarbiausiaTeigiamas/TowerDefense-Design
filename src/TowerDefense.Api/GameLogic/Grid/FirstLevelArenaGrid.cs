namespace TowerDefense.Api.GameLogic.Grid
{
    public class FirstLevelArenaGrid : IArenaGrid
    {
        public GridItem[] GridItems { get; set; } = new GridItem[Constants.TowerDefense.MaxGridGridItemsForPlayer];

        public FirstLevelArenaGrid()
        {
            IGridLayout layoutAdapter = GridLayoutFactory.CreateLayout();
            string gridLayout = layoutAdapter.GetFormattedLayout();
            GridItems.CreateGrid(gridLayout);
        }
    }
}
