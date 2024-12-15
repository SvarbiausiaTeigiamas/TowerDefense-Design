namespace TowerDefense.Api.GameLogic.Grid
{
    public abstract class ArenaGridLoader : IArenaGrid
    {
        public GridItem[] GridItems { get; set; } =
            new GridItem[Constants.TowerDefense.MaxGridGridItemsForPlayer];

        // Template method - sealed to prevent override
        public void LoadGrid(string filePath)
        {
            ValidateFile(filePath);
            string gridLayout = ReadFile(filePath);
            ParseGridLayout(gridLayout);
            GridItems.CreateGrid(gridLayout);
            PostProcessGrid();
        }

        // Required abstract methods
        protected abstract void ValidateFile(string filePath);
        protected abstract string ReadFile(string filePath);
        protected abstract void ParseGridLayout(string gridLayout);

        // Optional hook method
        protected virtual void PostProcessGrid() { }
    }
}
