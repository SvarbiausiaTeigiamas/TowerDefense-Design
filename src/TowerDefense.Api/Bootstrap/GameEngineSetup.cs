using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.GameLogic.Player.Memento;
using TowerDefense.Api.Hubs;

namespace TowerDefense.Api.Bootstrap
{
    public static class GameEngineSetup
    {
        public static void SetupGameEngine(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<NotificationHub>(); 
            serviceCollection.AddTransient<INotificationHub>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<NotificationHubProxy>>();
                return new NotificationHubProxy(
                    serviceProvider.GetRequiredService<NotificationHub>,
                    logger
                );
            });
            serviceCollection.AddTransient<IShopHandler, ShopHandler>();
            serviceCollection.AddTransient<ITurnHandler, TurnHandler>();
            serviceCollection.AddTransient<IBattleHandler, BattleHandler>();
            serviceCollection.AddTransient<IInitialGameSetupHandler, InitialGameSetupHandler>();
            serviceCollection.AddTransient<IInventoryHandler, InventoryHandler>();
            serviceCollection.AddTransient<IGridHandler, GridHandler>();
            serviceCollection.AddTransient<IPlayerHandler, PlayerHandler>();
            serviceCollection.AddTransient<IAttackHandler, AttackHandler>();
            serviceCollection.AddTransient<IGameHandler, GameHandler>();
            serviceCollection.AddSingleton<ICareTaker, CareTaker>();
            serviceCollection.AddTransient<IPerkHandler, PerkHandler>();
        }
    }
}
