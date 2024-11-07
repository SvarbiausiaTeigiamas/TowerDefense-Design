namespace TowerDefense.Api.GameLogic.Grid
{
    public class FirstLevelArenaGrid : IArenaGrid
    {
        public GridItem[] GridItems { get; set; } = new GridItem[Constants.TowerDefense.MaxGridGridItemsForPlayer];
        private readonly GridLayoutFactory _layoutFactory;

        // Default constructor for backward compatibility
        public FirstLevelArenaGrid() : this(GridLayoutFactory.Instance)
        {
        }

        // Constructor injection for testability
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
