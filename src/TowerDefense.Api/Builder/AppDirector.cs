namespace TowerDefense.Api.Builder {

public class AppDirector 
    {
        private readonly IWebAppBuilder _builder;

        public AppDirector(IWebAppBuilder builder) {
            _builder = builder;
        }

        public WebApplication ConstructApp() {
            return _builder
                .AddControllers()
                .AddSignalR()
                .AddSwagger()
                .SetupGameEngine()
                .SetupAutoMapper()
                .AddCorsPolicy()
                .Build();
        }
    }
}