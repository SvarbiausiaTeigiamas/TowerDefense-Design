namespace TowerDefense.Api.GameLogic.Grid
{
    public class JsonLayoutAdapter : IGridLayout
    {
        private readonly JsonLayout _jsonLayout;

        public JsonLayoutAdapter(string jsonFilePath)
        {
            _jsonLayout = new JsonLayout(jsonFilePath);
        }

        public string GetFormattedLayout()
        {
            return _jsonLayout.Layout;
        }
    }
}
