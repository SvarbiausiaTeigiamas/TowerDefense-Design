using System;
using System.Threading.Tasks;

namespace TowerDefense.Api.GameLogic.GameState
{
    public class WaitingForPlayersState : IGameState
    {
        private readonly GameContext _context;
        private readonly State _gameState;

        public WaitingForPlayersState(GameContext context, State gameState)
        {
            Console.WriteLine("WaitingForPlayersState: Initialized");
            _context = context;
            _gameState = gameState;
        }

        public void AddPlayer(string playerName)
        {
            Console.WriteLine(
                $"WaitingForPlayersState: A new player '{playerName}' is now in the game. ActivePlayers: {_gameState.ActivePlayers}"
            );
            if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
            {
                Console.WriteLine(
                    "WaitingForPlayersState: Max players reached. Ready to transition to InProgressState when TryStartGame is called."
                );
            }
        }

        public Task TryStartGame()
        {
            Console.WriteLine(
                "WaitingForPlayersState: TryStartGame called. Checking if we can transition..."
            );
            if (_gameState.ActivePlayers == Constants.TowerDefense.MaxNumberOfPlayers)
            {
                Console.WriteLine(
                    "WaitingForPlayersState: Conditions met, transitioning to InProgressState..."
                );
                _context.SetState(_context.InProgressState);
            }
            else
            {
                Console.WriteLine("WaitingForPlayersState: Still waiting for more players...");
            }
            return Task.CompletedTask;
        }

        public Task EndTurn(string playerName)
        {
            Console.WriteLine(
                "WaitingForPlayersState: EndTurn called but we are still waiting for players. No action taken."
            );
            return Task.CompletedTask;
        }

        public Task FinishGame(string winnerName)
        {
            Console.WriteLine("WaitingForPlayersState: FinishGame called but no game in progress.");
            return Task.CompletedTask;
        }
    }
}
