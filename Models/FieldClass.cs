using System.Text;

public class FieldClass
{
    internal char[,] Field = new char[3, 3];
    readonly int _rows;
    readonly int _cols;

    public FieldClass()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Welcome to the Tic-Tac-Toe game!");

        _rows = Field.GetLength(0);
        _cols = Field.GetLength(1);

        SetStartField();
        DisplayField();
    }

    private void SetStartField()
    {
        for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _cols; j++)
            {
                Field[i, j] = '.';
            }
    }

    public void DisplayField()
    {
        for (int i = 0; i < _rows; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < _cols; j++)
            {
                if (j != 1)
                    Console.Write($" {Field[i, j]} ");
                else
                    Console.Write($"| {Field[i, j]} |");
            }

            Console.WriteLine();
            if (i != 2)
                Console.WriteLine(" _________ ");
        }
        Console.WriteLine();
    }
}

    
