namespace TowerDefense.Api.GameLogic.Grid
{
    public class GridLayoutFactory
    {
        public static IGridLayout CreateLayout(string jsonFilePath = "layout.json")
        {
            var jsonAdapter = new JsonLayoutAdapter(jsonFilePath);
            if (jsonAdapter.GetFormattedLayout() != null)
            {
                LoggerManager.Instance.LogInfo("Using Json Layout!!!!");
                return jsonAdapter;
            }
            else
            {
                LoggerManager.Instance.LogInfo("Using Constant Layout!!!!");
                return new ConstantLayoutAdapter();
            }
        }
    }
}
