namespace TowerDefense.Api.Builder {
    public interface IWebAppBuilder
    {
        IWebAppBuilder AddControllers();
        IWebAppBuilder AddSignalR();
        IWebAppBuilder AddSwagger();
        IWebAppBuilder SetupGameEngine();
        IWebAppBuilder SetupAutoMapper();
        IWebAppBuilder AddCorsPolicy();
        WebApplication Build();
    }
}