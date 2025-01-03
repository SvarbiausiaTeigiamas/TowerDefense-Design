using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TowerDefense.Api.Contracts.Command;
using TowerDefense.Api.Contracts.Player;
using TowerDefense.Api.Contracts.Turn;
using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Handlers;

namespace TowerDefense.Api.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IInitialGameSetupHandler _initialGameSetupHandler;
        private readonly IPlayerHandler _playerHandler;
        private readonly IMapper _mapper;
        private readonly IGameHandler _gameHandler;
        private readonly ITurnHandler _turnHandler;
        private readonly GameContext _gameContext;

        public PlayerController(
            IGameHandler gameHandler,
            IInitialGameSetupHandler initialGameSetupHandler,
            IPlayerHandler playerHandler,
            ITurnHandler turnHandler,
            IMapper mapper,
            GameContext gameContext
        )
        {
            _gameHandler = gameHandler;
            _initialGameSetupHandler = initialGameSetupHandler;
            _playerHandler = playerHandler;
            _turnHandler = turnHandler;
            _mapper = mapper;
            _gameContext = gameContext;
        }

        [HttpPost]
        public ActionResult<AddNewPlayerResponse> Register(
            [FromBody] AddNewPlayerRequest addPlayerRequest
        )
        {
            var player = _initialGameSetupHandler.AddNewPlayer(addPlayerRequest.PlayerName);
            _gameContext.AddPlayer(addPlayerRequest.PlayerName); // Inform the state machine

            var addNewPlayerResponse = _mapper.Map<AddNewPlayerResponse>(player);

            return Ok(addNewPlayerResponse);
        }

        [HttpGet("{playerName}")]
        public ActionResult<GetPlayerInfoResponse> GetInfo(string playerName)
        {
            var player = _playerHandler.GetPlayer(playerName);
            var getPlayerInfoResponse = _mapper.Map<GetPlayerInfoResponse>(player);

            return Ok(getPlayerInfoResponse);
        }

        [HttpPost("endturn")]
        public ActionResult EndTurn(EndTurnRequest endTurnRequest)
        {
            _turnHandler.TryEndTurn(endTurnRequest.PlayerName);
            return Ok();
        }

        /// <summary>
        /// Clears game state and restarts game
        /// </summary>
        /// <returns></returns>
        [HttpPost("reset")]
        public ActionResult Reset()
        {
            _gameHandler.ResetGame();
            return Ok();
        }

        [HttpPost("place-item")]
        public ActionResult PlaceItemOnGrid(ExecuteCommandRequest request)
        {
            var player = _playerHandler.GetPlayer(request.PlayerName);

            var inventory = player.Inventory;
            var requestedItem = inventory.Children.FirstOrDefault(
                x => x.Id == request.InventoryItemId
            );

            if (requestedItem == null)
            {
                return Ok(new { Message = "Item not found in inventory." });
            }

            var playersGridItems = player.ArenaGrid.GridItems;
            var selectedGridItem = playersGridItems.FirstOrDefault(
                g => g.Id == request.GridItemId.Value
            );

            if (selectedGridItem == null)
            {
                return BadRequest(new { Message = "Invalid GridItemId." });
            }

            inventory.Remove(requestedItem); // Use Remove method
            selectedGridItem.Item = requestedItem;
            return Ok(new { Message = "Item placed successfully." });
        }

        [HttpPost("command")]
        public ActionResult ExecuteCommand(ExecuteCommandRequest commandRequest)
        {
            return Ok();
        }

        [HttpPost("command/text")]
        public ActionResult InterpretCommand(InterpretCommandRequest commandRequest)
        {
            return Ok();
        }
    }
}
