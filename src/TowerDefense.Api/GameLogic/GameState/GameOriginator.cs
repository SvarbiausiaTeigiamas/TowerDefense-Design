using TowerDefense.Api.GameLogic.Observers;

namespace TowerDefense.Api.GameLogic.GameState;

public static class GameOriginator
{
    private static readonly List<IGameObserver> Observers = new();

    private static State _gameState = new();
    public static State GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            NotifyObservers();
        }
    }

    public static void Attach(IGameObserver observer)
    {
        Observers.Add(observer);
    }

    public static void Detach(IGameObserver observer)
    {
        Observers.Remove(observer);
    }

    private static void NotifyObservers()
    {
        foreach (var observer in Observers)
        {
            observer.Update(_gameState);
        }
    }
}
