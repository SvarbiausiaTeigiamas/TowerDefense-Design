using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Player;
using TowerDefense.Api.GameLogic.Shop;

namespace TowerDefense.Api.GameLogic.Factories;

public interface IGameFactory
{
    IShop CreateShop();
    IGridLayout CreateGridLayout();
    IPlayer CreatePlayer(string name);
}
