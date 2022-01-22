[System.Serializable]
public class PlayerBank
{
    private uint _bankSize;
    public uint BankSize => _bankSize;

    public PlayerBank(uint bankSize)
    {
        _bankSize = bankSize;
    }

    public void AddMoney(uint money) => _bankSize += money;

    public bool RemoveMoney(uint money)
    {
        if (money > _bankSize)
            return false;

        else
        {
            _bankSize -= money;

            return true;
        }
    }
}
