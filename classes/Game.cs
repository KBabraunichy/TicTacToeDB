
class Game
{
    Player[] players = new Player[2];
    public Game(Player player1, Player player2)
    {
        players[0] = player1;
        players[1] = player2;
    }

    public void GameStart()
    {
        Console.WriteLine("Игра началась!\nИгроки вводят 2 числа, x и y, через пробел, от 1 до 3. Например (x, y) = (1, 1) является первой клеткой.");
        //Console.WriteLine("На каждый ход дается 15 секунд, если игрок не успевает, ход переходит другому игроку.");
        int whoseTurn = WhoseTurn();
        int turn = 1;
        string[] briefNames = { $"Ход игрока {players[0].Name}('x')", $"Ход игрока {players[1].Name}('o')" };
        
        while (turn <= 9)
        {
            Console.WriteLine(briefNames[whoseTurn]);
            players[whoseTurn].SetField();
            if (turn >= 5)
            {
                if (WinCheck(players[whoseTurn]))
                {
                    Console.WriteLine($"Победа игрока {players[whoseTurn].Name}!");
                    return;
                }

            }
            whoseTurn = (whoseTurn == 0) ? 1 : 0;
            turn++;
        }
        Console.WriteLine("Ходы закончились. Ничья!");
    }
    int WhoseTurn()
    {
        double randomnumber = new Random().NextDouble();
        if (randomnumber <= 0.5)
        {
            Console.WriteLine($"Игрок {players[0].Name}('x') ходит первым.");
            return 0;
        }
        Console.WriteLine($"Игрок {players[1].Name}('o') ходит первым.");
        return 1;
    }

    bool WinCheck(Player player)
    {
        char[,] field = player.field;
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

        int leftdiag = 0;
        int rightdiag = 0;
        //Диагонали
        for (int i = 0; i < 3; i++)
        {
            if (field[i, i] == type)
            {
                leftdiag++;
            }
            if (field[i, 2 - i] == type)
            {
                rightdiag++;
            }
        }
        if (leftdiag == 3 || rightdiag == 3)
        {
            return true;
        }

        return false;
    }

}