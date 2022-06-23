using Microsoft.EntityFrameworkCore;

public class PlayerRepository : IRepository<Player>
{
    private AppContext db;

    public PlayerRepository()
    {
        db = new AppContext();
    }

    public List<Player> GetObjectList()
    {
        return db.Players.ToList();
    }

    public Player GetObject(int id)
    {
        return db.Players.Find(id);
    }

    public void Create(Player player)
    {
        db.Players.Add(player);
    }

    public void Update(Player player)
    {
        db.Entry(player).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        Player player = GetObject(id);
        if (player != null)
            db.Players.Remove(player);
    }

    public void Save()
    {
        if (HasUnsavedChanges())
            db.SaveChanges();
    }
    public bool HasUnsavedChanges()
    {
        return db.ChangeTracker.HasChanges();
    }

}