public interface ITableBank : IBank
{
    public uint ChipsForTransferOnTable { get; }

    public uint DivideBankWithWinners(int playerCount, double minValue);
}
