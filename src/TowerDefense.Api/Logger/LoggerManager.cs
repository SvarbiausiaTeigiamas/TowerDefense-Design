public sealed class LoggerManager
{
    private static volatile LoggerManager instance;
    private static readonly object lockObject = new object();

    // Private constructor to prevent direct instantiation
    private LoggerManager() { }

    public static LoggerManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new LoggerManager();
                    }
                }
            }
            return instance;
        }
    }

    // Thread-safe logging methods using lock to prevent console color conflicts
    public void LogInfo(string message)
    {
        lock (lockObject)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Log("INFO", message);
        }
    }

    public void LogError(string message)
    {
        lock (lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log("ERROR", message);
        }
    }

    public void LogWarning(string message)
    {
        lock (lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Log("WARNING", message);
        }
    }

    public void LogSuccess(string message)
    {
        lock (lockObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Log("SUCCESS", message);
        }
    }

    private void Log(string level, string message)
    {
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}");
        Console.ResetColor();
    }
}
