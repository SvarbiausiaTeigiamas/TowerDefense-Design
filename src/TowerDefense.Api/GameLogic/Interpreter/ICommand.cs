using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public interface ICommand
{
    void Execute(string[] args = null);
    string Description { get; }
}