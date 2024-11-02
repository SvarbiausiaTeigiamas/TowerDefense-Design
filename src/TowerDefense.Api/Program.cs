// Program.cs
var builder = WebApplication.CreateBuilder(args);

var appBuilder = new AppBuilder(builder);
var app = appBuilder
    .AddControllers()
    .AddSignalR()
    .SetupGameEngine()
    .SetupAutoMapper()
    .AddCorsPolicy()
    .Build();

app.Run();