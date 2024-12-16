using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public interface IInitialGameSetupHandler
    {
        IPlayer AddNewPlayer(string playerName);
        void SetConnectionIdForPlayer(string playerName, string connectionId);
        IPlayer AddPlayerToGame(string playerName);
        void SetArenaGridForPlayer(IPlayer player);
        void SetShopForPlayer(IPlayer player);
        void SetPerkStorageForPlayer(IPlayer player);
        Task TryStartGame();
    }
}
