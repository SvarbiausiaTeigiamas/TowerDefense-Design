namespace TowerDefense.Api.GameLogic.Grid
{
    public class ConstantLayoutAdapter : IGridLayout
    {
        private readonly ConstantLayout _constantLayout;

        public ConstantLayoutAdapter()
        {
            _constantLayout = new ConstantLayout();
        }

        public string GetFormattedLayout()
        {
            return _constantLayout.Layout;
        }
    }
}
