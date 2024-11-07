// AppBuilder.cs
using TowerDefense.Api.Bootstrap;
using TowerDefense.Api.Bootstrap.AutoMapper;
using TowerDefense.Api.Constants;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.Builder {
    public class ProdWebAppBuilder : IWebAppBuilder
    {
        private readonly WebApplicationBuilder _builder;
        private WebApplication _app;

        public ProdWebAppBuilder(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
        }

        public IWebAppBuilder AddControllers()
        {
            _builder.Services.AddControllers();
            return this;
        }

        public IWebAppBuilder AddSignalR()
        {
            _builder.Services.AddSignalR();
            return this;
        }

        public IWebAppBuilder AddSwagger()
        {
            _builder.Services.AddSwaggerGen();
            _builder.Services.AddEndpointsApiExplorer();    
            return this;
        }

        public IWebAppBuilder SetupGameEngine()
        {
            _builder.Services.SetupGameEngine();
            return this;
        }

        public IWebAppBuilder SetupAutoMapper()
        {
            _builder.Services.SetupAutoMapper();
            return this;
        }

        public IWebAppBuilder AddCorsPolicy()
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
}
