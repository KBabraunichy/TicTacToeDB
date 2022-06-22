
using System.ComponentModel.DataAnnotations.Schema;

class Player
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PlayerId { get; set; }

    private string name;
    private int age;
    private char type;

    [Column(TypeName = "varchar(25)")]
    public string Name { get { return name; } set { name = value; } }
    public int Age { get { return age; } set { age = value; } }
    public char Type { get { return type; } set { type = value; } }

    public Player()
    {

    }

    public Player(char type)
    {
        Console.WriteLine($"Player '{type}':");
        this.type = type;
        EnterId();
        EnterName();
        EnterAge();
    }



    private void EnterName()
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

    private void EnterAge()
    {
        while (true)
        {
            Console.Write("Enter your age: ");

            try
            {
                int checkAge = int.Parse(Console.ReadLine());

                if (checkAge >= 10 && checkAge <= 90)
                {
                    age = checkAge;
                    return;
                }
                throw new Exception();
            }
            catch
            {
                Console.WriteLine("Incorrect age, enter the age between 10 and 90.");
            }   
        }
    }
    private void EnterId()
    {
        while (true)
        {
            Console.Write("Enter your ID: ");

            try
            {
                int checkId = int.Parse(Console.ReadLine());

                if (checkId > 0)
                {
                    PlayerId = checkId;
                    return;
                }
                throw new Exception();
            }
            catch
            {
                Console.WriteLine("Incorrect ID, enter an int number greater than 0.");
            }
        }
    }
}