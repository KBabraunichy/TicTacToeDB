using Microsoft.EntityFrameworkCore;

public class GameRepository : IRepository<Game>
{
    private AppContext db;

    public GameRepository()
    {
        db = new AppContext();
    }

    public List<Game> GetObjectList()
    {
        return db.Games.ToList();
    }

    public Game GetObject(int id)
    {
        return db.Games.Find(id);
    }

    public Game GetLastObjectById()
    {
        return db.Games.OrderByDescending(p => p.GameId).FirstOrDefault();
    }

    public void Create(Game game)
    {
        db.Games.Add(game);
    }

    public void Update(Game game)
    {
        db.Entry(game).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        Game game = GetObject(id);
        if (game != null)
            db.Games.Remove(game);
    }

    public void Save()
    {
        if(HasUnsavedChanges())
            db.SaveChanges();
    }
    public bool HasUnsavedChanges()
    {
        return db.ChangeTracker.HasChanges();
    }
}