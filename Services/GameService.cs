using static TicTacToe.Utils.GameServiceConstants;
using System.Text.Json;
using System.Text.RegularExpressions;

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

    public bool GameResultAsync()
    {
        string[] availableCommands = new[] { GenerateResultsCommand, GenerateAllResultsCommand, NewGameCommand, CloseAppCommand };
        while (true)
        {
            Console.WriteLine("\nYou can enter next commands:" +
                              $"\n{GenerateResultsCommand} - create json file contains info about last game" +
                              $"\n{GenerateAllResultsCommand} - create json file contains info about all games" +
                              $"\n{NewGameCommand} - start new game" +
                              $"\n{CloseAppCommand} - finish the game and close app");

            string command = Console.ReadLine().Trim();
            if (!availableCommands.Contains(command))
            {
                Console.WriteLine("\nIncorrect command, please try again.");
                continue;
            }
            else
            {

                if (command == NewGameCommand)
                    return true;

                if (command == CloseAppCommand)
                    return false;

                List<Game> gamesFromDB;
                string fileName;

                string dateTime = DateTime.Now.ToString();
                Regex reg = new Regex("[: .]");
                dateTime = reg.Replace(dateTime, "_");

                if (command == GenerateAllResultsCommand)
                {
                    gamesFromDB = _gameRepository.GetObjectList();
                    fileName = string.Concat(AllGamesFileName, "_", dateTime, FileFormat);
                }
                else
                {
                    gamesFromDB = new List<Game> { _gameRepository.GetLastObjectById() };
                    fileName = string.Concat(LastGameFileName, "_", dateTime, FileFormat);
                }

                GamesToJSON(gamesFromDB, fileName);

            }
        }
    }

    public async void GamesToJSON(List<Game> gamesFromDB, string fileName)
    {
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilesDirectoryName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            string json = JsonSerializer.Serialize(gamesFromDB);
            await writer.WriteLineAsync(json);

            Console.WriteLine("\nData has been saved to file:");
            Console.WriteLine(filePath);
            return;
        }
    }
}

