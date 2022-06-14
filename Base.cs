namespace Base
{

    public class FieldClass
    {
        internal char[,] field = new char[3, 3];
        readonly int rows;
        readonly int cols;
        public FieldClass()
        {
            rows = field.GetLength(0);
            cols = field.GetLength(1);
            SetStartField();
        }

        public void SetStartField()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    field[i, j] = '.';
                }
        }

        public void DisplayField()
        {
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < cols; j++)
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

    class Player : FieldClass
    {
        string? name;
        readonly char type;
        public Player(char type, ref FieldClass classfield)
        {
            this.type = type;
            field = classfield.field;
            EnterName();
        }
        public string? Name
        {
            get
            {
                return name;
            }
        }
        public char Type { get { return type; } }
        public void EnterName()
        {
            while (true)
            {
                Console.Write("Введите ваше имя: ");
                string? check = Console.ReadLine();
                if (!string.IsNullOrEmpty(check) && check.Length < 26)
                {
                    name = check;
                    return;
                }
                else
                    Console.WriteLine("Некорректное имя или превышена длина имени в 25 символов, попробуйте снова.");
            }
        }

        public void SetField(int attempt = 0)
        {
            int[] xy = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Console.Write($"Введите число {((i == 0) ? 'x' : 'y')}: ");
                xy[i] = EnterXYCheck() - 1;
            }
            //Проверка на заполненность клетки
            if (field[xy[0], xy[1]] == '.')
            {
                field[xy[0], xy[1]] = type;
                DisplayField();
            }
            else
            {
                //если игрок вводит 3 раза x и y, тогда ход переходит следующему игроку
                if (attempt == 2)
                {
                    Console.WriteLine("Введено 3 некорректных набор x и y, ход переходит другому игроку");
                    return;
                }
                attempt++;
                Console.WriteLine("Ошибка! Место занято, введите другие x и y.");
                SetField(attempt);
            }
        }
        static protected int EnterXYCheck()
        {
            while(true)
            {
                try
                {
                    int[] n = { 1, 2, 3 };
                    int number;
                    number = int.Parse(Console.ReadLine());
                    if (!n.Contains(number))
                        throw new Exception("Ошибка! Возможны только числа от 1 до 3.");
                    return number;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Повторите ввод.");
                    continue;
                }
            }

        }
    }
}