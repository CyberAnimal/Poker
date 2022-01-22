using System;
using System.Collections.Generic;
using System.Linq;

public struct Card : IEquatable<Card>
{
    private readonly CardType.Suit _suit;
    private readonly CardType.Rank _rank;

    public CardType.Suit Suit => _suit;
    public CardType.Rank Rank => _rank;

    public Card(CardType.Suit suit, CardType.Rank rank)
    {
        _suit = suit;
        _rank = rank;
    }

    public static bool operator >(Card first, Card second) => first.Rank > second.Rank;
    public static bool operator <(Card first, Card second) => first.Rank < second.Rank;

    public static bool operator ==(Card first, Card second) => first.Rank == second.Rank;
    public static bool operator !=(Card first, Card second) => first.Rank != second.Rank;

    public override int GetHashCode() => ((int)_suit + 2) ^ ((int)_rank + 2);
    public override bool Equals(object obj)
    {
        if ((obj is Card) == false)
            return false;

        return Equals((Card)obj);
    }
    bool IEquatable<Card>.Equals(Card other) => _suit == other.Suit &&
                                                _rank == other.Rank;
}

public static class ExtentionCard
{
    public static List<Card> SortSuits(this List<Card> cards)
    {
        List<Card> hearts = new List<Card>();
        List<Card> diamonds = new List<Card>();
        List<Card> spades = new List<Card>();
        List<Card> clubs = new List<Card>();

        List<List<Card>> suits = new List<List<Card>>();
        suits.Add(hearts);
        suits.Add(diamonds);
        suits.Add(spades);
        suits.Add(clubs);

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Suit == CardType.Suit.HEART)
                hearts.Add(cards[i]);

            else if (cards[i].Suit == CardType.Suit.DIAMONDS)
                diamonds.Add(cards[i]);

            else if(cards[i].Suit == CardType.Suit.SPADES)
                spades.Add(cards[i]);

            else if(cards[i].Suit == CardType.Suit.CLUBS)
                clubs.Add(cards[i]);
        }

        List<Card> suitFiveOrMore = new List<Card>();

        for (int i = 0; i < suits.Count; i++)
            if (suits[i].Count >= 5)
                suitFiveOrMore = suits[i];

        return suitFiveOrMore;
    }

    public static List<Card> SortRanks(this List<Card> cards) => (List<Card>)cards.OrderByDescending(x => (int)x.Rank);
}
