using System;
using System.Collections.Generic;

[Serializable]
public class Deck : IDeck
{
    private readonly List<Card> _cards;
    public List<Card> Cards => _cards;

    private int _deckCount = 52;

    public Deck()
    {
        _cards = new List<Card>();
        _cards.Capacity = _deckCount;

        foreach (CardType.Suit suit in Enum.GetValues(typeof(CardType.Suit)))
            foreach (CardType.Rank rank in Enum.GetValues(typeof(CardType.Rank)))
            {
                if (_cards.Count >= _deckCount)
                    return;

                _cards.Add(new Card(suit, rank));
            } 
    }

    public Card GiveAwayCard()
    {
        Card card = _cards[_deckCount];

        for (int i = 0; i < 2; i++)
        {
            _cards.RemoveAt(_deckCount);
            --_deckCount;
        }

        return card;
    }

    public List<Card> Shuffle()
    {
        Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int n = _cards.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);

            Card value = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = value;
        }

        return _cards;
    }
}
