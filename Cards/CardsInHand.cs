public class CardsInHand
{
    private readonly Card[] cards;
    public Card[] Cards => cards;

    public CardsInHand(Card firstCard, Card secondCard)
    {
        cards = new Card[] 
        { 
            firstCard, 
            secondCard 
        };
    }
}
