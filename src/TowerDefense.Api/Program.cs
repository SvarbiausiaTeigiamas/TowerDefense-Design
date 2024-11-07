public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var appBuilder = new AppBuilder(builder);
        var app = appBuilder
            .AddControllers()
            .AddSignalR()
            .AddSwagger()
            .SetupGameEngine()
            .SetupAutoMapper()
            .AddCorsPolicy()
            .Build();

        app.Run();
    }
}