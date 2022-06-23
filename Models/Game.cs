using static TicTacToe.Utils.GameConstants;
using System.Text.RegularExpressions;

public class Game
{
    public delegate void GameHandler(string message);
    public event GameHandler? Notify;

    public int GameId { get; set; }
    public int FirstPlayerId { get; set; }
    public int SecondPlayerId { get; set; }
    public int WinnerPlayerId { get; set; } = 0;  // 0 - Draw
    public DateTime GameStartedTime { get; set; }
    public DateTime GameFinishedTime { get; set; }

    public Player[] players = new Player[2];
    private FieldModel gameField;

    public Game()
    {

    }

    public void GameStart()
    {
        gameField = new FieldModel();
        players[0] = new Player(FirstPlayerCharacter);
        players[1] = new Player(SecondPlayerCharacter);
        Notify += (string message) =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        };

        Notify?.Invoke("\nThe game has begun!\nPlayers enter 2 numbers, x and y, separated by a space, from 1 to 3." +
            " For example (x, y) = (1, 1) is the first square.");
        Notify?.Invoke("For each turn you have 15 seconds, if time's up, the turn goes to another player.");
        
        int whoseTurn = WhoseTurn();
        int turn = 1;
        string[] briefNames = { $"\nThe turn of the player {players[0].Name}('{players[0].Type}')", 
                                $"\nThe turn of the player {players[1].Name}('{players[1].Type}')" };

        FirstPlayerId = players[0].PlayerId;
        SecondPlayerId = players[1].PlayerId;

        GameStartedTime = DateTime.Now;

        while (turn <= 9)
        {
            Console.WriteLine(briefNames[whoseTurn]);

            //Due to possible turn's transitions to the opponent, I pass "turn" variable as a ref to keep "turn" always 
            //within 9 so as not to create additional methods/variables to check the field's fullness and 
            //avoid a draw with open squares possible for a win

            SetField(whoseTurn, ref turn);
            if (turn >= 5)
            {
                if (WinCheck(players[whoseTurn].Type))
                {
                    Notify?.Invoke($"The victory of the player {players[whoseTurn].Name}!");
                    WinnerPlayerId = players[whoseTurn].PlayerId;
                    break;
                }

            }
            whoseTurn = (whoseTurn == 0) ? 1 : 0;
            
            if(turn==10)
                Notify?.Invoke("The moves are over. A draw!");
        }

        GameFinishedTime = DateTime.Now;

    }

    private void SetField(int whoseTurn, ref int turn, int attempt = 0)
    {
        Console.Write($"Enter numbers x and y: ");
        EnterXYCheck(out int x, out int y);

        if (x == -1)
        {
            Console.WriteLine("\nTime's up.\nThe turn goes to another player.");
            return;
        }

        //checks if square is filled
        if (gameField.Field[x, y] == '.')
        {
            gameField.Field[x, y] = players[whoseTurn].Type;
            gameField.DisplayField();
            turn++;
        }
        else
        {
            //if player enters 3 incorrect sets of x and y, the turn goes to another player
            if (attempt == 2)
            {
                Console.WriteLine("You've entered 3 incorrect sets of x and y, the turn goes to another player.");
                return;
            }
            attempt++;

            Console.WriteLine("Error! The square is filled, enter another set of x and y.");
            SetField(whoseTurn, ref turn, attempt);
        }
    }
    static protected void EnterXYCheck(out int x, out int y)
    {
        int timer = 0;
        Regex regex = new Regex(@"\s+");
        while (true)
        {
            try
            {
                //Code below checks that x and y contain only numbers from 1 to 3. Also removes unnecessary space characters

                string stringCheck = "";
                if (Console.KeyAvailable)
                {
                    stringCheck = Console.ReadLine();
                    timer = 0;
                }

                Thread.Sleep(250);
                timer++;

                if (timer == 60)
                {
                    x = y = -1;
                    return;
                }
                else if (stringCheck == "")
                {
                    continue;
                }
                else
                {
                    stringCheck = regex.Replace(stringCheck.Trim(), " ");
                    if (!Regex.IsMatch(stringCheck, @"^[1-3]\s[1-3]$"))
                    {
                        throw new Exception("Error! Only numbers from 1 to 3 are possible.");
                    }
                    else
                    {
                        x = int.Parse(stringCheck[0].ToString()) - 1;
                        y = int.Parse(stringCheck[2].ToString()) - 1;
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("Re-enter x and y: ");
                //continue;
            }
        }

    }

    private int WhoseTurn()
    {
        double randomNumber = new Random().NextDouble();
        if (randomNumber <= 0.5)
        {
            Console.WriteLine($"Player {players[0].Name}('{players[0].Type}') goes first.");
            return 0;
        }
        Console.WriteLine($"Player {players[1].Name}('{players[1].Type}') goes first.");
        return 1;
    }

    private bool WinCheck(char type)
    {
        char[,] field = gameField.Field;
        int horizont;
        int vertical;

        for (int i = 0; i < 3; i++)
        {
            horizont = 0; vertical = 0;
            for (int j = 0; j < 3; j++)
            {
                if (field[i, j] == type)
                {
                    horizont++;
                }
                if (field[j, i] == type)
                {
                    vertical++;
                }
            }
            if (horizont == 3 || vertical == 3)
            {
                return true;
            }

        }

        int leftDiag = 0;
        int rightDiag = 0;
        //Diags
        for (int i = 0; i < 3; i++)
        {
            if (field[i, i] == type)
            {
                leftDiag++;
            }
            if (field[i, 2 - i] == type)
            {
                rightDiag++;
            }
        }
        if (leftDiag == 3 || rightDiag == 3)
        {
            return true;
        }

        return false;
    }

}