using System.Text;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine("Добро пожаловать в игру \"крестики-нолики\"!");
    FieldClass newField = new();
    newField.DisplayField();
    
    Console.WriteLine("Игрок 'x':");
    Player player1 = new ('x');
    
    Console.WriteLine("Игрок 'o':");
    Player player2 = new ('o');

    player1.field = player2.field = newField.field;

    Game startgame = new (player1, player2);
    startgame.GameStart();

    Console.WriteLine("Нажмите клавишу Enter, если хотите сыграть снова. Если хотите выйти, нажмите любую другую клавишу.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;
    Console.Clear();
}