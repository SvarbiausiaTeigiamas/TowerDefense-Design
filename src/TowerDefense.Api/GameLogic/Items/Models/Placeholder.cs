using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Visitor;

namespace TowerDefense.Api.GameLogic.Items.Models
{
    public class Placeholder : IItem, IVisitableItem
    {
        public string Id { get; set; } = nameof(Placeholder);
        public ItemType ItemType { get; set; } = ItemType.Placeholder;
        public IItemStats Stats { get; set; } = new DefaultZeroItemStats();
        public ICollection<string> PowerUps { get; set; } = new List<string>();

        public IEnumerable<AttackDeclaration> Attack(
            IArenaGrid opponentsArenaGrid,
            int attackingGridItemId
        )
        {
            var affectedGridItemList = ItemHelpers.GetAttackedGridItems(
                opponentsArenaGrid,
                attackingGridItemId
            );
            return affectedGridItemList.Select(x => new AttackDeclaration()
            {
                GridItemId = x,
                Damage = Stats.Damage,
            });
        }

        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
