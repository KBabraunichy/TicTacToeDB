using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

public class Player
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
        EnterPlayerInfo();
    }

    private void EnterPlayerInfo()
    {
        Console.WriteLine("Enter your info in next format: <ID> <Name> <Age> (e.g. 12 Denis 32).");
        while (true)
        {
            try
            {
                
                string stringCheck = Console.ReadLine();

                Regex regex = new Regex(@"\s+");
                stringCheck = regex.Replace(stringCheck.Trim(), " ");

                if (stringCheck.Where(x => (x == ' ')).Count() != 2 || !Regex.IsMatch(stringCheck, @"^\d+\s\S+\s\d+$"))
                    throw new Exception("Incorrect format, please try again.");

                string[] words = stringCheck.Split(' ');
                
                PlayerId = int.Parse(words[0]);
                name = words[1];
                age = int.Parse(words[2]);

                if (age < 10 || age > 90 || name.Length > 25)
                    throw new Exception("Incorrect age or name, please try again.");

                return;

            }
            catch (OverflowException)
            {
                Console.WriteLine("Your ID or age exceeds the maximum integer value, please try again.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}