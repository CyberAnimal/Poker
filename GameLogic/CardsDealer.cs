using System.Collections.Generic;
using UnityEngine;

public class CardsDealer : MonoBehaviour
{
    private Deck _deck;
    private CardsOnTable _cardsOnTable;
    public CardsOnTable CardsOnTable => _cardsOnTable;

    public void StartRound(List<Player> players)
    {
        _deck = new Deck();

        HandOutCards(players);
    }

    public void CardsLayOutToFlop()
    {
        _cardsOnTable = new CardsOnTable(_deck.GiveAwayCard(),
                                         _deck.GiveAwayCard(),
                                         _deck.GiveAwayCard());
    }

    public void CardsLayOutToTurnOrRiver()
    {
        _cardsOnTable.AddCard(_deck.GiveAwayCard());
    }

    private void HandOutCards(List<Player> players)
    {
        foreach (Player player in players)
        {
            Card firstCard = _deck.GiveAwayCard();
            Card secondCard = _deck.GiveAwayCard();

            player.TakeCards(firstCard, secondCard);
        }
    }
}
