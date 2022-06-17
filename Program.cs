using static dotNET_task1.Utils.GameConstants;

while (true)
{
    Game startGame = new(new FieldClass(), new Player(FirstPlayerCharacter), new Player(SecondPlayerCharacter));
    startGame.GameStart();

    Console.WriteLine("Press Enter if you want to play again. If you want to exit, press any other key.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;

    Console.Clear();
}