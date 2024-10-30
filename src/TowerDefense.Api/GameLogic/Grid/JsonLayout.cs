using System.Text.Json;

namespace TowerDefense.Api.GameLogic.Grid
{
    public class GridLayoutJson
    {
        public string Layout { get; set; }
    }

    public class JsonLayout
    {
        public string Layout { get; set; }

        public JsonLayout(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var layoutObject = JsonSerializer.Deserialize<GridLayoutJson>(jsonContent);
                Layout = layoutObject.Layout;
            }
        }
    }
}
