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
        try
        {
            _gameRepository.Create(game);
            _gameRepository.Save();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Game info has not been added in the database.");
        }
    }

    public bool GameResult()
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

                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }

    public async void GamesToJSON(List<Game> gamesFromDB, string fileName)
    {
        var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        string folderPath = Path.Combine(directory.Parent.Parent.Parent.ToString(), FilesDirectoryName);

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

