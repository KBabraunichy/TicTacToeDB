
class Player
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
}