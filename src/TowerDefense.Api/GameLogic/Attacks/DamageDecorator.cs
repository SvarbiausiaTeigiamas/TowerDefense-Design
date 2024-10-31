namespace TowerDefense.Api.GameLogic.Attacks
{
    public abstract class DamageDecorator : IDamageDecorator
    {
        protected readonly IDamage _damage;

        protected DamageDecorator(IDamage damage)
        {
            _damage = damage;
        }

        public virtual float Intensity
        {
            get => _damage.Intensity;
            set => _damage.Intensity = value;
        }

        public virtual int Size
        {
            get => _damage.Size;
            set => _damage.Size = value;
        }

        public virtual int Time
        {
            get => _damage.Time;
            set => _damage.Time = value;
        }

        public virtual DamageType DamageType
        {
            get => _damage.DamageType;
            set => _damage.DamageType = value;
        }

        public IDamage BaseDamage => _damage;
    }
}
