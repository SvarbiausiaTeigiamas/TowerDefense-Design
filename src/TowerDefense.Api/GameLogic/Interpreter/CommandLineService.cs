namespace TowerDefense.Api.GameLogic.Interpreter;

public class CommandLineService : BackgroundService
{
    private readonly CommandRegistry _registry;
    private readonly ILogger<CommandLineService> _logger;

    public CommandLineService(CommandRegistry registry, ILogger<CommandLineService> logger)
    {
        _registry = registry;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Command line service started");

        // Run command processing in a separate task
        _ = Task.Run(
            () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.Write("> ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                        continue;

                    var parts = input.Split(' ');
                    var commandName = parts[0].ToLower();
                    var args = parts.Skip(1).ToArray();

                    if (_registry.TryGetCommand(commandName, out ICommand? command))
                    {
                        try
                        {
                            command?.Execute(args);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error executing command {Command}", commandName);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Unknown command: {commandName}");
                    }
                }

                return Task.CompletedTask;
            },
            stoppingToken
        );

        // Keep the service alive without blocking
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
