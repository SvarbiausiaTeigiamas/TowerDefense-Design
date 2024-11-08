using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TowerDefense.Api.Contracts.Perks;
using TowerDefense.Api.GameLogic;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.Controllers
{
    [Route("api/perks")]
    [ApiController]
    public class PerksController : ControllerBase
    {
        private readonly GameFacade _gameFacade;
        private readonly IMapper _mapper;
        public PerksController(GameFacade gameFacade, IMapper mapper)
        {
            _gameFacade = gameFacade;
            _mapper = mapper;
        }

        [HttpGet("{playerName}")]
        public ActionResult<GetPerksResponse> GetPerks(string playerName)
        {
            var perks = _gameFacade.GetPlayerPerks(playerName);

            var perksResponse = _mapper.Map<GetPerksResponse>(perks);

            return Ok(perksResponse);
        }

        [HttpPost]
        public ActionResult PostPerk(ApplyPerkRequest applyPerkRequest)
        {
            _gameFacade.ApplyPerk(applyPerkRequest);

            return Ok();
        }
    }
}
