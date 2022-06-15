
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
        Console.WriteLine("На каждый ход дается 15 секунд, если игрок не успевает, ход переходит другому игроку.");
        
        int whoseTurn = WhoseTurn();
        int turn = 1;
        string[] briefNames = { $"Ход игрока {players[0].Name}('x')", $"Ход игрока {players[1].Name}('o')" };
        
        while (turn <= 9)
        {
            Console.WriteLine(briefNames[whoseTurn]);

            //Из-за возможных переходов хода к противнику, передаю turn как ссылку, чтобы держать turn всегда в пределах 9
            //чтобы не создавать дополнительных методов/переменных на проверку заполненности поля и избежать
            //ничьи с открытыми клетками, возможными под выйгрыш

            players[whoseTurn].SetField(ref turn);
            if (turn >= 5)
            {
                if (WinCheck(players[whoseTurn]))
                {
                    Console.WriteLine($"Победа игрока {players[whoseTurn].Name}!");
                    return;
                }

            }
            whoseTurn = (whoseTurn == 0) ? 1 : 0;
            //turn++;
        }
        Console.WriteLine("Ходы закончились. Ничья!");
    }
    int WhoseTurn()
    {
        double randomNumber = new Random().NextDouble();
        if (randomNumber <= 0.5)
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

        int leftDiag = 0;
        int rightDiag = 0;
        //Диагонали
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