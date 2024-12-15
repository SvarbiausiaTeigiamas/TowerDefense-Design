namespace TowerDefense.Api.GameLogic.Grid
{
    public sealed class ArenaGridLoaderFactory
    {
        public static ArenaGridLoader CreateLoader(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension switch
            {
                ".json" => new JsonArenaGridLoader(),
                ".csv" => new CsvArenaGridLoader(),
                _ => throw new ArgumentException($"Unsupported file type: {extension}"),
            };
        }
    }
}
