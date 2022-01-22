using System.Collections.Generic;

public class CardsOnTable
{
    private uint cardsLenght = 5;

    private readonly List<Card> _cards;
    public List<Card> Cards => _cards;

    public CardsOnTable(Card card1, Card card2, Card card3)
    {
        _cards = new List<Card>();

        _cards.Add(card1);
        _cards.Add(card2);
        _cards.Add(card3);
    }

    public void AddCard(Card card) => _cards.Add(card);
}