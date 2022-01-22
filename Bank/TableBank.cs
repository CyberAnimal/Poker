using System.Collections.Generic;
using System.Linq;

public class TableBank
{
    private List<PlayerRate> _playerRates;
    public List<PlayerRate> PlayerRates => _playerRates;

    private uint _bankSize;
    public uint BankSize => _bankSize;

    public TableBank(int playerCount)
    {
        _playerRates = new List<PlayerRate>(playerCount);
    }

    public void StartNewBetRound() => _playerRates = new List<PlayerRate>();
    public void AddToMoneyOnTable(Player player, uint betSize)
    {
        PlayerRate rate = new PlayerRate(player, betSize);

        if (_playerRates.Exists(x => x.Player == rate.Player))
            _playerRates.Select(x => x).Where(x => x.Player == rate.Player).Single().AddRate(rate);

        else
            _playerRates.Add(rate);

        _bankSize += rate.Money;
    }
    public int GetMaxSizeBet()
    {
        int maxCount = _playerRates.Max(x => x.Rates.Count);
        List<PlayerRate> lastRates = _playerRates.Where(x => x.Rates.Count == maxCount)
                                                 .Select(x => x.Rates[maxCount])
                                                 .ToList();

        int maxBet = (int)lastRates.Max(x => x.Money);

        return maxBet;
    }
    public void DeletePlayerFromBetRound(Player player)
    {
        PlayerRate rate = _playerRates.Select(x => x).Where(x => x.Player == player).Single();

        _playerRates.Remove(rate);
    }
    public Dictionary<Player, int> DivideBankWithWinners()
    {
        Dictionary<Player, int> moneyForPlayers = new Dictionary<Player, int>();
        List<int> allBetsSize = new List<int>();
        List<Player> playersForDivide = new List<Player>();

        foreach (var rate in _playerRates)
        {
            if (rate.Player.Rate == Rate.AllIn)
            {
                int betsSize = rate.GetAllBetSize();
                allBetsSize.Add(betsSize);
                moneyForPlayers.Add(rate.Player, betsSize);
            }

            else
                playersForDivide.Add(rate.Player);
        }

        float sum = allBetsSize.Sum();
        int bankForDivide = (int)(_bankSize - sum);
        int moneyForOnePlayer = bankForDivide / playersForDivide.Count;

        foreach (var player in playersForDivide)
            moneyForPlayers.Add(player, moneyForOnePlayer);

        return moneyForPlayers;
    }
}
