using TowerDefense.Api.Contracts.Perks;
using TowerDefense.Api.Contracts.Shop;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.GameLogic.PerkStorage;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Shop;

namespace TowerDefense.Api.GameLogic
{
    public class GameFacade
    {
        private readonly IInitialGameSetupHandler _initialGameSetupHandler;
        private readonly IInventoryHandler _inventoryHandler;
        private readonly IPlayerHandler _playerHandler;
        private readonly IGameHandler _gameHandler;
        private readonly ITurnHandler _turnHandler;
        private readonly IShopHandler _shopHandler;
        private readonly IPerkHandler _perkHandler;
        private readonly IGridHandler _gridHandler;

        public GameFacade(
            IInitialGameSetupHandler initialGameSetupHandler,
            IInventoryHandler inventoryHandler,
            IPlayerHandler playerHandler,
            IGameHandler gameHandler,
            ITurnHandler turnHandler,
            IShopHandler shopHandler,
            IPerkHandler perkHandler,
            IGridHandler gridHandler
            )
        {
            _initialGameSetupHandler = initialGameSetupHandler;
            _inventoryHandler = inventoryHandler;
            _playerHandler = playerHandler;
            _gameHandler = gameHandler;
            _turnHandler = turnHandler;
            _shopHandler = shopHandler;
            _perkHandler = perkHandler;
            _gridHandler = gridHandler;
        }

        public IPlayer AddNewPlayer(string connectionId)
        {
            return _initialGameSetupHandler.AddNewPlayer(connectionId);
        }

        public IPlayer GetPlayer(string playerName)
        {
            return _playerHandler.GetPlayer(playerName);
        }

        public void EndTurn(string playerName)
        {
            _turnHandler.TryEndTurn(playerName);
        }

        public bool BuyItem(string playerName, string itemId)
        {
            return _shopHandler.TryBuyItem(playerName, itemId);
        }

        public void ApplyPerk(string playerName, int perkId)
        {
            _perkHandler.UsePerk(playerName, perkId);
        }

        public void ResetGame()
        {
            _gameHandler.ResetGame();
        }

        public IArenaGrid GetGridItems(string playerName)
        {
            return _gridHandler.GetGridItems(playerName);
        }

        public Inventory GetPlayerInventory(string playerName)
        {
            return _inventoryHandler.GetPlayerInventory(playerName);
        }

        public IShop GetPlayerShop(string playerName)
        {
            return _shopHandler.GetPlayerShop(playerName);
        }

        public bool TryBuyItem(BuyShopItemRequest buyShopItemRequest)
        {
            return _shopHandler.TryBuyItem(buyShopItemRequest.PlayerName, buyShopItemRequest.ItemId);
        }

        public IPerkStorage GetPlayerPerks(string playerName)
        {
            return _perkHandler.GetPerks(playerName);
        }

        public void ApplyPerk(ApplyPerkRequest applyPerkRequest)
        {
            _perkHandler.UsePerk(applyPerkRequest.PlayerName, applyPerkRequest.PerkId);
        }
    }
}
