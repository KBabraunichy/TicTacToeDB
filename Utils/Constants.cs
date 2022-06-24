
namespace TicTacToe.Utils
{
    public static class GameConstants
    {
        public const char FirstPlayerCharacter = 'x';
        public const char SecondPlayerCharacter = 'o';
    }

    public static class GameServiceConstants
    {
        public const string NewGameCommand = "/newgame";
        public const string CloseAppCommand = "/close";
        public const string GenerateResultsCommand = "/generateresults";
        public const string GenerateAllResultsCommand = "/generateallresults";
        public const string LastGameFileName = "game";
        public const string AllGamesFileName = "games";
        public const string FilesDirectoryName = "GameFiles";
        public const string FileFormat = ".json";
    }
}
