namespace TowerDefense.Api.GameLogic.Perks
{
    public class ResetDamagePerk : IPerk
    {
        public int Id { get; init; }
        public string Name => "Reset Damage!";
        public PerkType Type => PerkType.ResetDamage;
    }
}
