using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Visitor
{
    public interface IItemVisitor
    {
        void Visit(Blank blank);
        void Visit(Placeholder placeholder);
        void Visit(Rockets rockets);
        void Visit(Shield shield);
        void Visit(Plane plane);
    }
}