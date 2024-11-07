namespace TowerDefense.Api.GameLogic.Grid
{
    public class FirstLevelArenaGrid : IArenaGrid
    {
        public GridItem[] GridItems { get; set; } = new GridItem[Constants.TowerDefense.MaxGridGridItemsForPlayer];

        private readonly GridLayoutFactory _layoutFactory;

        // Constructor injection for better testability and flexibility
        public FirstLevelArenaGrid(GridLayoutFactory layoutFactory)
        {
            _layoutFactory = layoutFactory;
            Initialize();
        }

        private void Initialize()
        {
            IGridLayout layoutAdapter = _layoutFactory.GetLayout();
            string gridLayout = layoutAdapter.GetFormattedLayout();
            GridItems.CreateGrid(gridLayout);
        }
    }
}
