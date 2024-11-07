using TowerDefense.Api.Bootstrap;
using TowerDefense.Api.Bootstrap.AutoMapper;
using TowerDefense.Api.Constants;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.Builder {
public class TestWebAppBuilder : IWebAppBuilder
    {
        private readonly WebApplicationBuilder _builder;
        private WebApplication _app;
        private readonly List<string> _configuredFeatures = new();

        public TestWebAppBuilder(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
        }

        public IWebAppBuilder AddControllers()
        {
            _builder.Services.AddControllers();
            _configuredFeatures.Add("Controllers");
            return this;
        }

        public IWebAppBuilder AddSignalR()
        {
            _builder.Services.AddSignalR();
            _configuredFeatures.Add("SignalR");
            return this;
        }

        public IWebAppBuilder AddSwagger()
        {
            // Skip Swagger in test environment
            _configuredFeatures.Add("Swagger-Skipped");
            return this;
        }

        public IWebAppBuilder SetupGameEngine()
        {
            _builder.Services.SetupGameEngine();
            _configuredFeatures.Add("GameEngine");
            return this;
        }

        public IWebAppBuilder SetupAutoMapper()
        {
            _builder.Services.SetupAutoMapper();
            _configuredFeatures.Add("AutoMapper");
            return this;
        }

        public IWebAppBuilder AddCorsPolicy()
        {
            _builder.Services.AddCors(options =>
            {
                options.AddPolicy(Policy.DevelopmentCors, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            _configuredFeatures.Add("CORS");
            return this;
        }

        public WebApplication Build()
        {
            LoggerManager.Instance.LogInfo($"Test WebApp building with features: {string.Join(", ", _configuredFeatures)}");
            
            _app = _builder.Build();

            // Minimal middleware for testing
            _app.UseRouting();
            _app.UseCors(Policy.DevelopmentCors);
            _app.MapControllers();
            _app.MapHub<GameHub>("api/gameHub");

            return _app;
        }

        // Helper method for testing
        public List<string> GetConfiguredFeatures() => _configuredFeatures;
    }
}