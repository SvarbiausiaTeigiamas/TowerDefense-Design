using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Handlers
{
    public class ArenaGridSetupHandler : PlayerSetupHandler
    {
        public ArenaGridSetupHandler(State gameState)
            : base(gameState) { }

        public override IPlayer Handle(IPlayer player)
        {
            string file = Path.GetFullPath("./GameLogic/Grid/CSVfile.csv");
            var arenaGrid = new FirstLevelArenaGrid(file);
            Console.WriteLine($"Chain of Responsibility: Arena grid set for player {player.Name}");
            Console.WriteLine("Using TEMPLATE pattern, reading from " + file);
            player.ArenaGrid = arenaGrid;

            return base.Handle(player);
        }
    }
}
