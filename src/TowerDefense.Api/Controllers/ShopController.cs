using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.Contracts.Shop;
using TowerDefense.Api.GameLogic;

namespace TowerDefense.Api.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly GameFacade _gameFacade;
        private readonly IMapper _mapper;

        public ShopController(GameFacade gameFacade, IMapper mapper)
        {
            _gameFacade = gameFacade;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpGet("{playerName}")]
        public ActionResult<GetShopItemsResponse> GetItems(string playerName)
        {
            var shop = _gameFacade.GetPlayerShop(playerName);
            var getShopItemsResponse = _mapper.Map<GetShopItemsResponse>(shop);

            return Ok(getShopItemsResponse);
        }

        [HttpPost]
        public ActionResult<BuyShopItemResponse> BuyItem(BuyShopItemRequest buyShopItemRequest)
        {
            var wasItemBought = _gameFacade.TryBuyItem(buyShopItemRequest);

            return Ok(new BuyShopItemResponse { WasBought = wasItemBought });
        }
    }
}
