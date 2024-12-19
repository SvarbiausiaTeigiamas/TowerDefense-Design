namespace TowerDefense.Api.GameLogic.Visitor
{
    public interface IVisitableItem
    {
        void Accept(IItemVisitor visitor);
    }
}
