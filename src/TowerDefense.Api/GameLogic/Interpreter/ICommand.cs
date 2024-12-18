using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public interface ICommand
{
    void Interpret(State state);
}
