using System.Text;

Console.OutputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine("Welcome to the Tic-Tac-Toe game!");
    
    FieldClass newField = new();
    newField.DisplayField();
    
    Console.WriteLine("Player 'x':");
    Player player1 = new ('x');
    
    Console.WriteLine("Player 'o':");
    Player player2 = new ('o');

    Game startGame = new (player1, player2, newField);
    startGame.GameStart();

    Console.WriteLine("Press Enter if you want to play again. If you want to exit, press any other key.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;
    Console.Clear();
}