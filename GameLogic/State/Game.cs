using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private uint PlayerCount;
    [SerializeField] private MovesChanger MovesChanger;
    [SerializeField] private GameState GameState;
    [SerializeField] private TablePlayRounds TablePlayRounds;
    [SerializeField] private CardsDealer CardsDealer;
    [SerializeField] private TableBank TableBank;

    void StartGame()
    {
        TableBank = new TableBank((int)PlayerCount);
        GameState = new GameState(new MovesChanger(TableBank), (int)PlayerCount);

        TablePlayRounds.StartGame();
    }
    void PlayOneRound()
    {
        PlayRound round = TablePlayRounds.Initial;

        GameState.StartRound();
        CardsDealer.StartRound(MovesChanger.ActivePlayers);

        GameState.Flop();
        CardsDealer.CardsLayOutToFlop();

        GameState.Tern();
        CardsDealer.CardsLayOutToTurnOrRiver();

        GameState.River();
        CardsDealer.CardsLayOutToTurnOrRiver();

        GameState.OpenHands();

        CheckCombination(MovesChanger.ActivePlayers);
        DivideBank(MovesChanger.ActivePlayers);

        TablePlayRounds.SaveRound(round);
    }

    private void CheckCombination(List<Player> players)
    {
        CombinationsComparer combinations = new CombinationsComparer(new CardCombinationsDefiner());
        combinations.CompareCombinations(players, CardsDealer.CardsOnTable);
    }

    private void DivideBank(List<Player> players)
    {
        Dictionary<Player, int> dividedBanksForWinners = TableBank.DivideBankWithWinners();

        foreach (Player player in players)
        {
            int winingMoney = dividedBanksForWinners.Where(x => x.Key == player)
                                                    .Select(x => x.Value)
                                                    .SingleOrDefault();

            player.Bank.AddMoney((uint)winingMoney);
        }
    }
}

