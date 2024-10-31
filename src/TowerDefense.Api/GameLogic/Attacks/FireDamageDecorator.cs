namespace TowerDefense.Api.GameLogic.Attacks
{
    public class FireDamageDecorator : DamageDecorator
    {
        private const float FIRE_INTENSITY_MULTIPLIER = 1.2f;
        private const int FIRE_TIME = 2;

        public FireDamageDecorator(IDamage damage)
            : base(damage)
        {
        }

        public override float Intensity
        {
            get => base.Intensity * FIRE_INTENSITY_MULTIPLIER;
            set => base.Intensity = value;
        }

        public override int Time
        {
            get => Math.Max(base.Time, FIRE_TIME);
            set => base.Time = value;
        }

        public override DamageType DamageType
        {
            get => DamageType.Fire;
            set => base.DamageType = value;
        }
    }
}
