using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Visitor;

namespace TowerDefense.Api.GameLogic.Items.Models
{
    public class Shield : IItem, IVisitableItem
    {
        public string Id { get; set; } = nameof(Shield);
        public ItemType ItemType { get; set; } = ItemType.Shield;
        public IItemStats Stats { get; set; } = new BasicDefenseItemStats();

        public IEnumerable<AttackDeclaration> Attack(
            IArenaGrid opponentsArenaGrid,
            int attackingGridItemId
        )
        {
            var affectedGridItemList = new List<int>();
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
