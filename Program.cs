global using Base;

while (true)
{
    Console.WriteLine("Добро пожаловать в игру \"крестики-нолики\"!");
    FieldClass newfield = new();
    newfield.DisplayField();
    Console.WriteLine("Игрок 'x':");
    Player player1 = new ('x', ref newfield);
    Console.WriteLine("Игрок 'o':");
    Player player2 = new ('o', ref newfield);

    Game startgame = new (ref player1, ref player2);
    startgame.GameT(WhoseTurn());

    int WhoseTurn()
    {
        double randomnumber = new Random().NextDouble();
        if (randomnumber <= 0.5)
        {
            Console.WriteLine($"Игрок {player1.Name}('x') ходит первым.");
            return 0;
        }
        Console.WriteLine($"Игрок {player2.Name}('o') ходит первым.");
        return 1;
    }
    Console.WriteLine("Нажмите клавишу Enter, если хотите сыграть снова. Если хотите выйти, нажмите любую другую клавишу.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;
    Console.Clear();
}