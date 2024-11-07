namespace TowerDefense.Api.GameLogic.Grid
{
    public abstract class GridLayoutFactory
    {
        protected abstract IGridLayout CreateLayoutAdapter();

        public IGridLayout GetLayout()
        {
            var layout = CreateLayoutAdapter();
            LoggerManager.Instance.LogInfo($"Using {layout.GetType().Name}");
            return layout;
        }

        // Static factory instance for default usage
        private static readonly GridLayoutFactory DefaultFactory = new JsonGridLayoutFactory();
        public static GridLayoutFactory Instance => DefaultFactory;
    }

    // Concrete Creators
    public class JsonGridLayoutFactory : GridLayoutFactory
    {
        private readonly string _jsonFilePath;

        public JsonGridLayoutFactory(string jsonFilePath = "GameLogic/Grid/layout.json")
        {
            _jsonFilePath = jsonFilePath;
        }

        protected override IGridLayout CreateLayoutAdapter()
        {
            var adapter = new JsonLayoutAdapter(_jsonFilePath);
            return adapter.GetFormattedLayout() != null ? adapter : new ConstantLayoutAdapter();
        }
    }

    public class ConstantGridLayoutFactory : GridLayoutFactory
    {
        protected override IGridLayout CreateLayoutAdapter()
        {
            return new ConstantLayoutAdapter();
        }
    }
}
