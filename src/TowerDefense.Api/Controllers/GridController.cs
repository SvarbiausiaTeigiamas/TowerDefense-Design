using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TowerDefense.Api.Contracts.Grid;
using TowerDefense.Api.GameLogic;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.Controllers
{

    [Route("api/grid")]
    [ApiController]
    public class GridController : ControllerBase
    {
        private readonly GameFacade _gameFacade;
        private readonly IMapper _mapper;

        public GridController(GameFacade gameFacade, IMapper mapper)
        {
            _gameFacade = gameFacade;
            _mapper = mapper;
        }

        [HttpGet("{playerName}")]
        public ActionResult<GetGridResponse> GetGrid(string playerName)
        {
            var arenaGrid = _gameFacade.GetGridItems(playerName);

            var getGridResponse = _mapper.Map<GetGridResponse>(arenaGrid);

            return Ok(getGridResponse);
        }
    }
}
