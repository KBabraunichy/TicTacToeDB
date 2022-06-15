using System.Text.RegularExpressions;
class Player : FieldClass
{
    string name;
    readonly char type;
    public Player(char type)
    {
        this.type = type;
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
    void EnterName()
    {
        while (true)
        {
            Console.Write("Введите ваше имя: ");
            string check = Console.ReadLine();
            if (!string.IsNullOrEmpty(check) && check.Length < 26)
            {
                name = check;
                return;
            }
            else
                Console.WriteLine("Некорректное имя или превышена длина имени в 25 символов, попробуйте снова.");
        }
    }

    public void SetField(ref int turn, int attempt = 0)
    {
        Console.Write($"Введите числа x и y: ");
        EnterXYCheck(out int x, out int y);

        if (x == -1)
        {
            Console.WriteLine("\nВремя на ход истекло.\nХод переходит другому игроку.");
            return;
        }

        //Проверка на заполненность клетки
        if (field[x, y] == '.')
        {
            field[x, y] = type;
            DisplayField();
            turn++;
        }
        else
        {
            //если игрок вводит 3 раза занятую клетку, тогда ход переходит следующему игроку
            if (attempt == 2)
            {
                Console.WriteLine("Введено 3 некорректных набора x и y, ход переходит другому игроку");
                return;
            }
            attempt++;

            Console.WriteLine("Ошибка! Место занято, введите другие x и y.");
            SetField(ref turn, attempt);
        }
    }
    static protected void EnterXYCheck(out int x, out int y)
    {
        int timer = 0;
        Regex regex = new Regex(@"\s+");
        while (true)
        {
            try
            {
                //Код ниже помимо проверки, что x и y могут быть только от 1 до 3, убирает лишние пробелы, если таковы были
                
                string stringCheck = "";
                if (Console.KeyAvailable)
                {
                    stringCheck = Console.ReadLine();
                }

                Thread.Sleep(250);
                timer++;

                if (timer == 60)
                {
                    x = y = -1;
                    return;
                }
                else if (stringCheck == "")
                {
                    continue;
                }
                else
                {
                    stringCheck = regex.Replace(stringCheck.Trim(), " ");
                    if (!Regex.IsMatch(stringCheck, @"^[1-3]\s[1-3]$"))
                    {
                        throw new Exception("Ошибка! Возможны только два числа от 1 до 3.");
                    }
                    else
                    {
                        x = int.Parse(stringCheck[0].ToString()) - 1;
                        y = int.Parse(stringCheck[2].ToString()) - 1;
                        return;
                    }
                }
 
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("Повторите ввод x и y: ");
                //continue;
            }
        }

    }
}