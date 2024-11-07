using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.Contracts.Inventory;
using TowerDefense.Api.GameLogic;

namespace TowerDefense.Api.Controllers
{

    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly GameFacade _gameFacade;
        private readonly IMapper _mapper;
        public InventoryController(GameFacade gameFacade, IMapper mapper)
        {
            _gameFacade = gameFacade;
            _mapper = mapper;
        }

        [HttpGet("{playerName}")]
        public ActionResult<GetInventoryItemsResponse> GetItems(string playerName)
        {
            var inventory = _gameFacade.GetPlayerInventory(playerName);

            var getInventoryItemsResponse = _mapper.Map<GetInventoryItemsResponse>(inventory);

            return Ok(getInventoryItemsResponse);
        }
    }
}
