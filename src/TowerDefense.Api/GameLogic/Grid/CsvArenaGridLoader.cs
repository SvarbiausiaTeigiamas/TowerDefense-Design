namespace TowerDefense.Api.GameLogic.Grid
{
    public sealed class CsvArenaGridLoader : ArenaGridLoader
    {
        protected override void ValidateFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Grid layout file not found: {filePath}");

            if (!Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("File must be a CSV file");
        }

        protected override string ReadFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            return string.Join("\n", lines);
        }

        protected override void ParseGridLayout(string gridLayout)
        {
            // Convert CSV format to required format
            gridLayout = gridLayout.Replace(",", "");
        }
    }
}
