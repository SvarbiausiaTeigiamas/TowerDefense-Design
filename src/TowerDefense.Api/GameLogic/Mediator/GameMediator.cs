// src/TowerDefense.Api/GameLogic/Mediator/GameMediator.cs
using TowerDefense.Api.GameLogic.GameState;
using TowerDefense.Api.GameLogic.Handlers;
using TowerDefense.Api.GameLogic.Player;

namespace TowerDefense.Api.GameLogic.Mediator
{
    public class GameMediator : IMediator
    {
        public GameMediator()
        {
        }

        public async Task Notify(object sender, string eventCode)
        {
            Console.WriteLine($"Mediator: Event {eventCode} received");
            switch (eventCode)
            {
                case "EndTurn":
                    var players = sender as IPlayer[];
                    foreach (var iPlayer in players)
                    {
                        Console.WriteLine($"Mediator: Player {iPlayer.Name} turn ended");
                    }
                    break;
                case "PlayerAdded":
                    var player = sender as IPlayer;
                    Console.WriteLine($"Mediator: Player {player.Name} added to game state");
                    break;
            }
        }
    }
}
