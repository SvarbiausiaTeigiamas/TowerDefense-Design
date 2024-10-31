namespace TowerDefense.Api.GameLogic.Attacks
{
    public interface IDamageDecorator : IDamage
    {
        IDamage BaseDamage { get; }
    }
}
