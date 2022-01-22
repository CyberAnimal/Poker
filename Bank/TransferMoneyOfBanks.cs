using System.Linq;

public class TransferMoneyOfBanks
{
    private TableBank _tableBank;

    public TransferMoneyOfBanks(TableBank tableBank)
    {
        _tableBank = tableBank;
    }

    public int GetChangeSimple(int[] values, int change)
    {
        int count = 0;

        foreach (int value in values.Distinct().OrderByDescending(x => x))
        {
            count += change / value;
            change = change % value;

            if (change == 0) 
                return count;
        }

        return 0;
    }
}