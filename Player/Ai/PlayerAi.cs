using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerAiAction))]
public class PlayerAi : Player
{
    private PlayerAction _actionComponent;
    public override PlayerAction Action => _actionComponent;

    private PlayerBank _bank;
    public override PlayerBank Bank => _bank;

    private CardsInHand _cards;
    public override CardsInHand Cards => _cards;

    private SelfPlayerPosition _position;
    public override SelfPlayerPosition Position { get; set; }

    public override Rate Rate => throw new NotImplementedException();

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
        
    }

    private void Start()
    {
        _actionComponent = gameObject.GetComponent<PlayerAiAction>();
    }

    public override bool IsActive() => _bank.BankSize > 0;
}
