
public class FieldClass
{
    internal char[,] field = new char[3, 3];
    readonly int _rows;
    readonly int _cols;
    public FieldClass()
    {
        _rows = field.GetLength(0);
        _cols = field.GetLength(1);
        SetStartField();
    }

    void SetStartField()
    {
        for (int i = 0; i < _rows; i++)
            for (int j = 0; j < _cols; j++)
            {
                field[i, j] = '.';
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
                    Console.Write($" {field[i, j]} ");
                else
                    Console.Write($"| {field[i, j]} |");
            }

            Console.WriteLine();
            if (i != 2)
                Console.WriteLine(" _________ ");
        }
        Console.WriteLine();
    }
}

    
