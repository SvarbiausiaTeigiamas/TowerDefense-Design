using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Items;
using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Grid
{
    public class GridItem
    {
        private readonly Random _random = new Random();
        private const double CRITICAL_CHANCE = 0.30; // 30% chance for critical hit
        private const double EXPLOSION_CHANCE = 0.20; // 20% chance for explosion

        public int Id { get; set; }
        public IItem Item { get; set; }
        public ItemType ItemType => Item.ItemType;

        public AttackResult HandleAttack(AttackDeclaration attackDeclaration)
        {
            if (Item.Stats is not DefaultZeroItemStats)
            {
                this.Item.Stats.Health -= attackDeclaration.Damage;
            }

            bool isDestroyed = Item.Stats.Health <= 0;
            if (isDestroyed)
            {
                this.Item = new Blank();
            }

            // Create base damage
            IDamage damage = new Damage();

            // Apply fire damage decorator
            damage = new FireDamageDecorator(damage);

            // 20% chance to make it explosive
            if (_random.NextDouble() <= EXPLOSION_CHANCE)
            {
                damage = new ExplosionDecorator(damage);
            }

            // 30% chance to make it critical
            if (_random.NextDouble() <= CRITICAL_CHANCE)
            {
                damage = new CriticalDamageDecorator(damage);
            }

            return new AttackResult { GridId = this.Id, Damage = damage };
        }
    }
}
