namespace TowerDefense.Api.GameLogic.Attacks
{
    public class CriticalDamageDecorator : DamageDecorator
    {
        private const float CRITICAL_MULTIPLIER = 2.0f;

        public CriticalDamageDecorator(IDamage damage)
            : base(damage)
        {
        }

        public override float Intensity
        {
            get => base.Intensity * CRITICAL_MULTIPLIER;
            set => base.Intensity = value;
        }

        public override DamageType DamageType
        {
            get => DamageType.Critical;
            set => base.DamageType = value;
        }
    }
}
