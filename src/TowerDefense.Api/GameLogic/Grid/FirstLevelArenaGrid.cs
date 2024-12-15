namespace TowerDefense.Api.GameLogic.Grid
{
    public sealed class FirstLevelArenaGrid : IArenaGrid
    {
        public GridItem[] GridItems { get; set; }

        public FirstLevelArenaGrid(string filePath)
        {
            var gridLoader = ArenaGridLoaderFactory.CreateLoader(filePath);
            gridLoader.LoadGrid(filePath);
            GridItems = gridLoader.GridItems;
        }
    }
}
