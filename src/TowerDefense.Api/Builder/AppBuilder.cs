// AppBuilder.cs
using TowerDefense.Api.Bootstrap;
using TowerDefense.Api.Bootstrap.AutoMapper;
using TowerDefense.Api.Constants;
using TowerDefense.Api.Hubs;

// AppBuilder.cs
public class AppBuilder
{
    private readonly WebApplicationBuilder _builder;
    private WebApplication _app;

    public AppBuilder(WebApplicationBuilder builder)
    {
        _builder = builder;
    }

    public AppBuilder AddControllers()
    {
        _builder.Services.AddControllers();
        return this;
    }

    public AppBuilder AddSignalR()
    {
        _builder.Services.AddSignalR();
        return this;
    }

    public AppBuilder SetupGameEngine()
    {
        _builder.Services.SetupGameEngine();
        return this;
    }

    public AppBuilder SetupAutoMapper()
    {
        _builder.Services.SetupAutoMapper();
        return this;
    }

    public AppBuilder AddCorsPolicy()
    {
        _builder.Services.AddCors(options =>
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
        return this;
    }

    public WebApplication Build()
    {
        _app = _builder.Build();

        LoggerManager.Instance.LogInfo("WebApp was built");

        if (_app.Environment.IsDevelopment())
        {
            _app.UseSwagger();
            _app.UseSwaggerUI();
        }

        LoggerManager.Instance.LogInfo("Configured HTTP request pipeline");

        _app.UseCors(Policy.DevelopmentCors);
        _app.UseHttpsRedirection();
        _app.UseAuthorization();
        _app.MapControllers();
        _app.MapHub<GameHub>("api/gameHub");

        LoggerManager.Instance.LogInfo("Running the app");

        return _app;
    }
}