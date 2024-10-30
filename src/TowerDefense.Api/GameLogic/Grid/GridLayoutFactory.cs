namespace TowerDefense.Api.GameLogic.Grid
{
    public class GridLayoutFactory
    {
        public static IGridLayout CreateLayout(string jsonFilePath = "layout.json")
        {
            var jsonAdapter = new JsonLayoutAdapter(jsonFilePath);
            if (jsonAdapter.GetFormattedLayout() != null)
            {
                return jsonAdapter;
            }
            return new ConstantLayoutAdapter();
        }
    }
}
