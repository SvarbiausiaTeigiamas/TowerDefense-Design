public sealed class LoggerManager
{
    private static readonly LoggerManager instance = new();
    
    // Private constructor to prevent direct instantiation
    private LoggerManager() { }

    public static LoggerManager Instance => instance;

    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Log("INFO", message);
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Log("ERROR", message);
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Log("WARNING", message);
    }

    public void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Log("SUCCESS", message);
    }

    private void Log(string level, string message)
    {
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}");
        Console.ResetColor();
    }
}