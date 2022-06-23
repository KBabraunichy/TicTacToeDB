
while (true)
{
    Game startGame = new();
    startGame.GameStart();

    PlayerService playerService = new PlayerService();
    playerService.UpdateOrCreatePlayer(startGame.players);


    GameService gameService = new GameService();
    gameService.CreateGame(startGame);
    gameService.GamesResultToJSON();

    Console.WriteLine("\nPress Enter if you want to play again. If you want to exit, press any other key.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;

    Console.Clear();
}