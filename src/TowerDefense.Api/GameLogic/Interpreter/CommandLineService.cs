using TowerDefense.Api.GameLogic.GameState;

namespace TowerDefense.Api.GameLogic.Interpreter;

public class CommandLineService : BackgroundService
{
    private readonly ILogger<CommandLineService> _logger;
    private readonly CommandParser _parser;

    public CommandLineService(ILogger<CommandLineService> logger)
    {
        _logger = logger;
        _parser = new CommandParser();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Command line service started");

        _ = Task.Run(
            () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.Write("> ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                        continue;

                    try
                    {
                        var expression = _parser.Parse(input);
                        expression.Interpret(GameOriginator.GameState);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error executing command");
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                return Task.CompletedTask;
            },
            stoppingToken
        );

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
