﻿
while (true)
{
    Game startGame = new("init");
    startGame.GameStart();

    Console.WriteLine("\nPress Enter if you want to play again. If you want to exit, press any other key.");
    if(Console.ReadKey().Key != ConsoleKey.Enter)
        break;

    Console.Clear();
}