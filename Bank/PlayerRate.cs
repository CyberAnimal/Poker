using System.Collections.Generic;
using System.Linq;

public struct PlayerRate
{
    private List<PlayerRate> _rates;
    public List<PlayerRate> Rates => _rates;

    private readonly Player _player;
    public Player Player => _player;

    private readonly uint _money;
    public uint Money => _money;

    public PlayerRate(Player player, uint money = 0)
    {
        _rates = new List<PlayerRate>();
        _player = player;
        _money = money;
    }

    public void AddRate(PlayerRate rate) => _rates.Add(rate);

    public int GetAllBetSize() => (int)_rates.Sum(x => x.Money);
}