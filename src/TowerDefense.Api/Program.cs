using TowerDefense.Api.Bootstrap;
using TowerDefense.Api.Bootstrap.AutoMapper;
using TowerDefense.Api.Constants;
using TowerDefense.Api.GameLogic.Interpreter;
using TowerDefense.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.SetupGameEngine();

// Set up Interpreter pattern
builder.Services.AddSingleton<CommandRegistry>();
builder.Services.AddHostedService<CommandLineService>();
var registry = new CommandRegistry();
registry.RegisterCommand("help", new HelpCommand(registry));
registry.RegisterCommand("reset-health", new ResetHealthCommand());
registry.RegisterCommand("add-cash", new AddCashCommand());
builder.Services.AddSingleton(registry);

builder.Services.SetupAutoMapper();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        Policy.DevelopmentCors,
        builder =>
        {
            builder
                .WithOrigins("https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((x) => true)
                .AllowCredentials();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy.DevelopmentCors);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("api/gameHub");

app.Run();
