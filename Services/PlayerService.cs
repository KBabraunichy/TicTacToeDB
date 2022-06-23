public class PlayerService
{
    private readonly PlayerRepository _playerRepository;

    public PlayerService()
    {
        _playerRepository = new PlayerRepository();
    }

    public void UpdateOrCreatePlayer(Player[] players)
    {
        foreach (int i in new int[] { 0, 1 })
        {
            Player playerFromDB = _playerRepository.GetObject(players[i].PlayerId);
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
}