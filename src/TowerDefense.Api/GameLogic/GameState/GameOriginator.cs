using TowerDefense.Api.GameLogic.Player.Memento;

namespace TowerDefense.Api.GameLogic.GameState
{
    public static class GameOriginator
    {
        public static State GameState { get; set; } = new();

        public static IMemento SaveHealthSnapshot()
        {
            var player1 = GameState.Players[0];
            var player2 = GameState.Players[1];

            return new PlayerHealthMemento(new HealthSnapshot(player1.Health, player2.Health));
        }
    }
}
