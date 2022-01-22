using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerHumanAction))]
public class PlayerHuman : Player
{
    private PlayerAction _actionComponent;
    public override PlayerAction Action => _actionComponent;

    private PlayerBank _bank;
    public override PlayerBank Bank => _bank;

    private CardsInHand _cards;
    public override CardsInHand Cards => _cards;

    private SelfPlayerPosition _position;
    public override SelfPlayerPosition Position { get; set; }

    private Rate _rate;
    public override Rate Rate => _rate;

    public void AddMoneyInBank(uint moneyForBank)
    {
        if (_bank == null)
            _bank = new PlayerBank(moneyForBank);

        else _bank.AddMoney(moneyForBank);
    }

    public override void TakeCards(Card firstCard, Card secondCard) => _cards = new CardsInHand(firstCard, secondCard);

    public void RecordPosition(SelfPlayerPosition position) => _position = position;

    public override async Task<(bool isPlayerMoved, uint betMoney)> MakeStep(int moneyForCall)
    {
        Task<(bool isPlayerMoved, uint betMoney, Rate updatedRate)> finishedTask = 
            await Task.WhenAny(_actionComponent.CanStep(moneyForCall));

        Rate newRate = finishedTask.Result.updatedRate;
        _rate = newRate;

        bool isPlayerMoved = finishedTask.Result.isPlayerMoved;
        uint betMoney = finishedTask.Result.betMoney;

        return (isPlayerMoved, betMoney); 
    }

    private void Start()
    {
        _actionComponent = gameObject.GetComponent<PlayerHumanAction>();
        _actionComponent.CanStart();
    }

    public override bool IsActive() => _bank.BankSize > 0 && Rate != Rate.AllIn;
}
