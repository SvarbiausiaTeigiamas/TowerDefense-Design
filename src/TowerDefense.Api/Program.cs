using TowerDefense.Api.Bootstrap;
using TowerDefense.Api.Bootstrap.AutoMapper;
using TowerDefense.Api.Constants;
using TowerDefense.Api.Hubs;

LoggerManager.Instance.LogInfo("Starting the application");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.SetupGameEngine();
builder.Services.SetupAutoMapper();

builder.Services.AddCors(options =>
{
    options.AddPolicy(Policy.DevelopmentCors, builder =>
    {
        builder.WithOrigins("https://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((x) => true)
               .AllowCredentials();
    });
});

var app = builder.Build();

LoggerManager.Instance.LogInfo("WebApp was built");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

LoggerManager.Instance.LogInfo("Configured HTTP request pipeline");


app.UseCors(Policy.DevelopmentCors);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("api/gameHub");

LoggerManager.Instance.LogInfo("Running the app");
app.Run();
