using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Observers;

public interface IGameObserver
{
    void Update(State gameState);
}
