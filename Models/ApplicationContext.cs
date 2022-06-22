using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    internal DbSet<Player> Players { get; set; }
    internal DbSet<Game> Games { get; set; }

    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TicTacToeDB;Trusted_Connection=True;");
        //optionsBuilder.LogTo(System.Console.WriteLine);
    }
}
