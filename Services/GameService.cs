using static TicTacToe.Utils.GameServiceConstants;
using System.Text.Json;

public class GameService
{

    private readonly GameRepository _gameRepository;

    public GameService()
    {
        _gameRepository = new GameRepository();
    }

    public void CreateGame(Game game)
    {
        _gameRepository.Create(game);
        _gameRepository.Save();
    }

    public async void GamesResultToJSON()
    {
        string[] availableCommands = new[] { GenerateResultsCommand, GenerateAllResultsCommand, SkipCommand };
        while (true)
        {
            Console.WriteLine("\nYou can enter next commands:" +
                              $"\n{GenerateResultsCommand} - create json file contains info about last game" +
                              $"\n{GenerateAllResultsCommand} - create json file contains info about all games" +
                              $"\n{SkipCommand} - skip this part and continue");

            string command = Console.ReadLine().Trim();
            if (!availableCommands.Contains(command))
            {
                Console.WriteLine("Incorrect command, please try again.");
                continue;
            }
            else
            {
                if (command == SkipCommand)
                    return;

                List<Game> gamesFromDB;

                if (command == GenerateAllResultsCommand)
                    gamesFromDB = _gameRepository.GetObjectList();
                else
                    gamesFromDB = new List<Game> { _gameRepository.GetLastObjectId() };

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string json = JsonSerializer.Serialize(gamesFromDB);
                    await writer.WriteLineAsync(json);

                    Console.WriteLine("\nData has been saved to file:");
                    Console.WriteLine(path);
                    return;
                }

            }
        }
    }
}

