using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class SequenceCommand : ICommand
{
    private readonly List<ICommand> _expressions;

    public SequenceCommand(List<ICommand> expressions)
    {
        _expressions = expressions;
    }

    public void Interpret(State state)
    {
        foreach (var expression in _expressions)
        {
            expression.Interpret(state);
        }
    }
}
