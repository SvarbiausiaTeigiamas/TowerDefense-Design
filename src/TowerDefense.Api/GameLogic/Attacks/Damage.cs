namespace TowerDefense.Api.GameLogic.Attacks
{
    public class Damage : IDamage
    {
        public virtual float Intensity { get; set; } = 1;
        public virtual int Size { get; set; } = 1;
        public virtual int Time { get; set; } = 1;
        public virtual DamageType DamageType { get; set; } = DamageType.Projectile;
    }
}
