
while (true)
{
    Game startGame = new();
    startGame.GameStart();

    PlayerService playerService = new PlayerService();
    playerService.UpdateOrCreatePlayer(startGame.players);


    GameService gameService = new GameService();
    gameService.CreateGame(startGame);
    if(!gameService.GameResultAsync())
        break;

    Console.Clear();
}