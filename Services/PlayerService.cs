public class PlayerService
{
    private readonly PlayerRepository _playerRepository;

    public PlayerService()
    {
        _playerRepository = new PlayerRepository();
    }

    public void UpdateOrCreatePlayer(Player[] players)
    {
        try
        {
            var allPlayersFromDB = _playerRepository.GetObjectList();
            foreach (int i in new int[] { 0, 1 })
            {
                Player playerFromDB = allPlayersFromDB.Find(p => p.PlayerId == players[i].PlayerId);
                if (playerFromDB != null)
                {
                    if (!playerFromDB.Type.Equals(players[i].Type) || !playerFromDB.Name.Equals(players[i].Name)
                        || !playerFromDB.Age.Equals(players[i].Age))
                    {
                        playerFromDB.Name = players[i].Name;
                        playerFromDB.Age = players[i].Age;
                        playerFromDB.Type = players[i].Type;

                        _playerRepository.Update(playerFromDB);
                    }
                }
                else
                {
                    _playerRepository.Create(players[i]);
                }
            }

            _playerRepository.Save();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Players info have not been added nor updated in the database.");
        }

    }
}