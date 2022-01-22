using UnityEngine;

public class KingClubs : MonoBehaviour, ICardComponent
{
    private CardType.Suit _suit;
    private CardType.Rank _rank;

    public CardType.Suit Suit => _suit;
    public CardType.Rank Rank => _rank;

    private void Awake()
    {
        _suit = CardType.Suit.CLUBS;
        _rank = CardType.Rank.KING;
    }
}
