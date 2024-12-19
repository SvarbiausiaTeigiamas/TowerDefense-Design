using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Visitor
{
    public class ItemPriceCalculator : IItemVisitor
    {
        public void Visit(Blank blank)
        {
            // Calculate price for blank item
            blank.Stats.Price = blank.Stats.Damage * 2;
        }

        public void Visit(Placeholder placeholder)
        {
            // Calculate price for placeholder item
            placeholder.Stats.Price = (int)(placeholder.Stats.Damage * 1.5);
        }

        public void Visit(Rockets rockets)
        {
            // Calculate price for rockets item
            rockets.Stats.Price = rockets.Stats.Damage * 3;
        }

        public void Visit(Shield shield)
        {
            // Calculate price for shield item
            shield.Stats.Price = shield.Stats.Damage * 4;
        }

        public void Visit(Plane plane)
        {
            // Calculate price for plane item
            plane.Stats.Price = plane.Stats.Damage * 5;
        }
    }
}
