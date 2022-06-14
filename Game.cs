class Game
{
    readonly Player player1;
    readonly Player player2;
    public Game(ref Player player1, ref Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
    }

    public void GameT(int whoseturn)
    {
        Console.WriteLine("Игра началась!\nИгроки вводят 2 числа, x и y, от 1 до 3. Например (x, y) = (1, 1) является первой клеткой.");
        //Console.WriteLine("На каждый ход дается 15 секунд, если игрок не успевает, ход переходит другому игроку.");
        int turn = 1;
        string[] briefnames = { $"Ход игрока {player1.Name}('x')", $"Ход игрока {player2.Name}('o')" };
        Player[] players = { player1, player2 };
        while (turn <= 9)
        {
            Console.WriteLine(briefnames[whoseturn]);
            players[whoseturn].SetField();
            if (turn >= 5)
            {
                if (WinCheck(players[whoseturn]))
                {
                    Console.WriteLine($"Победа игрока {players[whoseturn].Name}!");
                    return;
                }

            }
            whoseturn = (whoseturn == 0) ? 1 : 0;
            turn++;
        }
        Console.WriteLine("Ходы закончились. Ничья!");
    }

    static bool WinCheck(Player player)
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