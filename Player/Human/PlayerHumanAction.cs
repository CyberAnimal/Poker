using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Slider))]
public class PlayerHumanAction : PlayerAction
{
    [SerializeField] private Button Call;
    [SerializeField] private Button Raise;
    [SerializeField] private Button Allin;
    [SerializeField] private Button Fold;
    [SerializeField] private Slider Slider;

    private enum ActionState
    {
        Wait,
        Step
    }
    private ActionState _actionState = ActionState.Wait;

    private Rate _newRate;

    public Action BetDoned;

    private PlayerBank _playerBank;
    private PlayerHuman _player;

    private uint _moneyForCall = default;
    private uint _betMoney = default;

    private bool _isBetDone;

    private Canvas _canvas;
    private Slider _slider;

    public override void CanStart()
    {
        _canvas = GetComponent<Canvas>(); //Получение компонента Canvas
        _canvas.enabled = false; //Отключение инвентаря при старте

        _slider = gameObject.GetComponent<Slider>();
        _slider.minValue = _moneyForCall * 2;
        _slider.maxValue = _player.Bank.BankSize;


        Call.onClick.AddListener(MakeCall);
        Raise.onClick.AddListener(MakeRaise);
        Allin.onClick.AddListener(MakeAllin);
        Fold.onClick.AddListener(MakeFold);
    }

    public override async Task<(bool isPlayerMoved, uint betMoney, Rate updatedRate)> CanStep(int moneyForCall)
    {
        _betMoney = default;
        _moneyForCall = (uint)moneyForCall;
        _actionState = ActionState.Step;

        while (_actionState == ActionState.Step)
            await Task.Yield();

        _moneyForCall = default;

        return (isPlayerMoved: true, betMoney: _betMoney, updatedRate: _newRate);
    }

    private void MakeCall()
    {
        if (_moneyForCall > _player.Bank.BankSize)
            return;

        if (_moneyForCall == _player.Bank.BankSize)
            _newRate = Rate.AllIn;

        _betMoney = _moneyForCall;

        _canvas.enabled = !_canvas.enabled;
        _newRate = Rate.Call;
        _actionState = ActionState.Wait;
    }

    private void MakeRaise()
    {
        uint moneyForRate = (uint)_slider.value;

        if (moneyForRate == _player.Bank.BankSize)
            _newRate = Rate.AllIn;

        else
            _newRate = Rate.Raise;

        _betMoney = moneyForRate;

        _canvas.enabled = !_canvas.enabled;
        _actionState = ActionState.Wait;
    }

    private void MakeAllin()
    {
        uint moneyForRate = _player.Bank.BankSize;

        _betMoney = moneyForRate;

        _canvas.enabled = !_canvas.enabled;
        _newRate = Rate.AllIn;
        _actionState = ActionState.Wait;
    }

    private void MakeFold()
    {
        _betMoney = 0;

        _canvas.enabled = !_canvas.enabled;
        _newRate = Rate.Fold;
        _actionState = ActionState.Wait;
    }
}
