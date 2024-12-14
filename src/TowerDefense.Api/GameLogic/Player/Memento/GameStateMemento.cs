using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Player.Memento;

public readonly struct HealthSnapshot
{
    public int Player1Health { get; }
    public int Player2Health { get; }

    public HealthSnapshot(int player1Health, int player2Health)
    {
        Player1Health = player1Health;
        Player2Health = player2Health;
    }
}

public class PlayerHealthMemento : IMemento
{
    private readonly HealthSnapshot _healthSnapshot;

    public PlayerHealthMemento(HealthSnapshot snapshot)
    {
        _healthSnapshot = snapshot;
    }

    public void Restore()
    {
        var player1 = GameOriginator.GameState.Players[0];
        var player2 = GameOriginator.GameState.Players[1];

        player1.Health = _healthSnapshot.Player1Health;
        player2.Health = _healthSnapshot.Player2Health;
    }
}
