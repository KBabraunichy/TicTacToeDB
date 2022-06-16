using System.Text.RegularExpressions;
class Player : FieldClass
{
    private string name;
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
            Console.Write("Enter your name: ");
            string check = Console.ReadLine();
            if (!string.IsNullOrEmpty(check) && check.Length < 26)
            {
                name = check;
                return;
            }
            else
                Console.WriteLine("Invalid name or the length of the name exceeds 25 characters, try again.");
        }
    }

    public void SetField(ref int turn, int attempt = 0)
    {
        Console.Write($"Enter numbers x and y: ");
        EnterXYCheck(out int x, out int y);

        if (x == -1)
        {
            Console.WriteLine("\nTime's up.\nThe turn goes to another player.");
            return;
        }

        //checks if square is filled
        if (Field[x, y] == '.')
        {
            Field[x, y] = type;
            DisplayField();
            turn++;
        }
        else
        {
            //if player enters 3 incorrect sets of x and y, the turn goes to another player
            if (attempt == 2)
            {
                Console.WriteLine("You've entered 3 incorrect sets of x and y, the turn goes to another player.");
                return;
            }
            attempt++;

            Console.WriteLine("Error! The square is filled, enter another set of x and y.");
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
                //Code below checks that x and y contain only numbers from 1 to 3. Also removes unnecessary space characters

                string stringCheck = "";
                if (Console.KeyAvailable)
                {
                    stringCheck = Console.ReadLine();
                    timer = 0;
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
                        throw new Exception("Error! Only numbers from 1 to 3 are possible.");
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
                Console.Write("Re-enter x and y: ");
                //continue;
            }
        }

    }
}