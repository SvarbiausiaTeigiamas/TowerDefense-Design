namespace TowerDefense.Api.GameLogic.Player.Memento;

public interface ICareTaker
{
    void Clear();
    void AddSnapshot(IMemento memento);
    IMemento GetPreviousState();
}

public class CareTaker : ICareTaker
{
    private readonly Stack<IMemento> _snapshotHistory = new();

    public void Clear()
    {
        _snapshotHistory.Clear();
    }

    public void AddSnapshot(IMemento memento)
    {
        _snapshotHistory.Push(memento);
    }

    public IMemento GetPreviousState()
    {
        _snapshotHistory.Pop();
        return _snapshotHistory.Peek();
    }
}
