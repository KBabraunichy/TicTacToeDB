
class Game
{
    private Player[] players = new Player[2];
    public Game(Player player1, Player player2)
    {
        players[0] = player1;
        players[1] = player2;
    }

    public void GameStart()
    {
        Console.WriteLine("\nThe game has begun!\nPlayers enter 2 numbers, x and y, separated by a space, from 1 to 3." +
            " For example (x, y) = (1, 1) is the first square.");
        Console.WriteLine("For each turn you have 15 seconds, if time's up, the turn goes to another player.");
        
        int whoseTurn = WhoseTurn();
        int turn = 1;
        string[] briefNames = { $"\nThe turn of the player {players[0].Name}('x')", $"\nThe turn of the player {players[1].Name}('o')" };
        
        while (turn <= 9)
        {
            Console.WriteLine(briefNames[whoseTurn]);

            //Due to possible turn's transitions to the opponent, I pass "turn" variable as a ref to keep "turn" always 
            //within 9 so as not to create additional methods/variables to check the field's fullness and 
            //avoid a draw with open squares possible for a win

            players[whoseTurn].SetField(ref turn);
            if (turn >= 5)
            {
                if (WinCheck(players[whoseTurn]))
                {
                    Console.WriteLine($"The victory of the player {players[whoseTurn].Name}!");
                    return;
                }

            }
            whoseTurn = (whoseTurn == 0) ? 1 : 0;
            //turn++;
        }
        Console.WriteLine("The moves are over. A draw!");
    }
    private int WhoseTurn()
    {
        double randomNumber = new Random().NextDouble();
        if (randomNumber <= 0.5)
        {
            Console.WriteLine($"Player {players[0].Name}('x') goes first.");
            return 0;
        }
        Console.WriteLine($"Player {players[1].Name}('o') goes first.");
        return 1;
    }

    private bool WinCheck(Player player)
    {
        char[,] field = player.Field;
        char type = player.Type;
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