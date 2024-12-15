using System.Text.Json;

namespace TowerDefense.Api.GameLogic.Grid
{
    public sealed class JsonArenaGridLoader : ArenaGridLoader
    {
        protected override void ValidateFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Grid layout file not found: {filePath}");

            if (!Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("File must be a JSON file");
        }

        protected override string ReadFile(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            var array = JsonSerializer.Deserialize<int[][]>(jsonContent);
            return string.Join("\n", array.Select(row => string.Join("", row)));
        }

        protected override void ParseGridLayout(string gridLayout)
        {
            gridLayout = gridLayout.Trim();
        }

        private class GridData
        {
            public string Layout { get; set; }
        }
    }
}
