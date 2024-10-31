using System.Text.Json;

namespace TowerDefense.Api.GameLogic.Grid
{
    public class JsonLayout
    {
        public string Layout { get; set; }

        public JsonLayout(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var array = JsonSerializer.Deserialize<int[][]>(jsonContent);
                Layout = string.Join("\n", array.Select(row => string.Join("", row)));
            }
        }
    }
}
