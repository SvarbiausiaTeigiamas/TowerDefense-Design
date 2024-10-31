namespace TowerDefense.Api.GameLogic.Attacks
{
    public class ExplosionDecorator : DamageDecorator
    {
        private const int EXPLOSION_SIZE_INCREASE = 2;

        public ExplosionDecorator(IDamage damage)
            : base(damage)
        {
        }

        public override int Size
        {
            get => base.Size + EXPLOSION_SIZE_INCREASE;
            set => base.Size = value;
        }

        public override DamageType DamageType
        {
            get => DamageType.Explosion;
            set => base.DamageType = value;
        }
    }
}
