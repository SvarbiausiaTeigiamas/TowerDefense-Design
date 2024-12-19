using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Visitor;

namespace TowerDefense.Api.GameLogic.Items
{
    public interface IItem : IVisitableItem
    {
        string Id { get; set; }
        IItemStats Stats { get; set; }
        ItemType ItemType { get; set; }
        IEnumerable<AttackDeclaration> Attack(
            IArenaGrid opponentsArenaGrid,
            int attackingGridItemId
        );
    }
}
