using Microsoft.EntityFrameworkCore;
using System.Configuration;


public class AppContext : DbContext
{
    internal DbSet<Player> Players { get; set; }
    internal DbSet<Game> Games { get; set; }

    public AppContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connection = ConfigurationManager.AppSettings["connectionString"];
        optionsBuilder.UseSqlServer(connection);
        //optionsBuilder.LogTo(System.Console.WriteLine);
    }
}
