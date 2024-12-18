namespace TowerDefense.Api.GameLogic.Interpreter;

public class CommandParser
{
    public ICommand Parse(string input)
    {
        var parts = input
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim())
            .ToList();

        if (parts.Count > 1)
        {
            var expressions = parts.Select(ParseSingleExpression).ToList();
            return new SequenceCommand(expressions);
        }

        return ParseSingleExpression(input);
    }

    private ICommand ParseSingleExpression(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return parts[0].ToLower() switch
        {
            "help" => new HelpCommand(),
            "add-cash" => new AddCashCommand(int.Parse(parts[1])),
            "reset-health" => new ResetHealthCommand(),
            _ => throw new ArgumentException($"Unknown command: {parts[0]}"),
        };
    }
}
