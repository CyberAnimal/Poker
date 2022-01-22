using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MovesChanger
{
    [SerializeField] private AllPlayerPositions _allPlayerPositions;
    [SerializeField] private PlayerSteps _playerSteps;
    [SerializeField] private PlayerStepState _playerStepState;

    private TableBank _tableBank;

    private Dictionary<Player, ActionState> _activePlayers;
    public List<Player> ActivePlayers => _activePlayers.Keys.ToList();

    public event Action PlayerWined;

    public MovesChanger(TableBank bank) => _tableBank = bank;

    public void Initialize(int playersCount)
    {
        _allPlayerPositions = new AllPlayerPositions(playersCount);
        InitializePlayers();
    }

    void InitializePlayers()
    {
        _activePlayers = new Dictionary<Player, ActionState>();

        List<ActionState> curentStates = InitialPositions(_allPlayerPositions.Players.Count);

        for (int i = 0; i < curentStates.Count; i++)
            _activePlayers.Add(_allPlayerPositions.Players[i], curentStates[i]);
    }

    List<ActionState> InitialPositions(int playersCount)
    {
        List<ActionState> curentStates = new List<ActionState>();
        curentStates.Add(ActionState.MB);
        curentStates.Add(ActionState.BB);

        for (int i = 0; i < playersCount - 2; i++)
            curentStates.Add(ActionState.ContinuePlay);

        return curentStates;
    }

    public List<Player> PlayersMove()
    {
        StepCurentPlayer();
        CircleOfBet();

        if (_raisePlayers.Count == 0)
            return ActivePlayers;

        else if (_activePlayers.Count == 1)
        {
            PlayerWined?.Invoke();

            return ActivePlayers;
        }
            

        else return PlayersMove();
    }

    public void CanNewRound()
    {
        _allPlayerPositions.CorrectionPlayersList();
        InitializePlayers();
    }

    async void StepCurentPlayer()
    {
        foreach (Player player in _activePlayers.Keys)
        {
            int maxBet = _tableBank.GetMaxSizeBet();
            bool isPlayerMoved = false;

            (bool isPlayerMoveCallBack, uint betMoney) = await player.MakeStep(maxBet);
            isPlayerMoved = isPlayerMoveCallBack;

            while (isPlayerMoved == false)
            {
                await Task.Yield();
            }

            _tableBank.AddToMoneyOnTable(player, betMoney);
        }
    }
    void CircleOfBet()
    {
        foreach (Player player in _activePlayers.Keys)
            CorrectingState(player);

        CorrectingPlayersList();

        List<int> playerRaisePositions = CheckMoveToContinues();

        if (playerRaisePositions.Count <= 1)
            return;

        UpdateRates(playerRaisePositions);
    }

    ActionState CorrectingState(Player player)
    {
        if (player.Rate == Rate.Fold)
            _activePlayers[player] = _activePlayers[player].SwitchState(Trigger.Fold);

        if (player.Rate == Rate.Check || player.Rate == Rate.Call)
            _activePlayers[player] = _activePlayers[player].SwitchState(Trigger.CheckOrCall);

        if (player.Rate == Rate.Raise || player.Rate == Rate.AllIn)
            _activePlayers[player] = _activePlayers[player].SwitchState(Trigger.RaiseOrAllin);

        return _activePlayers[player];
    }

    void CorrectingPlayersList()
    {
        if (_activePlayers.Count >= 1)
        {
            foreach (var item in _activePlayers.Where(x => x.Value == ActionState.Stop))
                _activePlayers.Remove(item.Key);
        }
    }

    List<int> CheckMoveToContinues()
    {
        List<int> playerRaisePositions = new List<int>();
        int count = 0;

        foreach (ActionState state in _activePlayers.Values)
        {
            count++;

            if (state == ActionState.RaiseRate)
                playerRaisePositions.Add(count);
        }

        return playerRaisePositions;
    }

    async void UpdateRates(List<int> playerRaisePositions)
    {
        int count = 0;

        foreach (Player player in _activePlayers.Keys)
        {
            count++;

            if (count < playerRaisePositions.Count)
            {
                int maxBet = _tableBank.GetMaxSizeBet();
                bool isPlayerMoved = false;

                (bool isPlayerMoveCallBack, uint betMoney) = await player.MakeStep(maxBet);

                isPlayerMoved = isPlayerMoveCallBack;

                while (isPlayerMoved == false)
                    await Task.Yield();

                _tableBank.AddToMoneyOnTable(player, betMoney);
            }

        }
    }
}