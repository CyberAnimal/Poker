using System.Collections.Generic;
using System.Linq;

public class CardCombinationsDefiner
{
    public Combination CheckCombination(List<Card> cards)
    {
        List<Card> oneSuitCards = cards.SortSuits();

        if (oneSuitCards.Count >= 5)
            return CheckFlushCombination(oneSuitCards);

        else if (StraightOrStraightFlush(cards))
            return Combination.Straight;

        else if (Repeat(cards))
            return CheckRepeatCombination(cards);

        return Combination.None;
    }

    Combination CheckFlushCombination(List<Card> cards)
    {
        cards = cards.SortRanks();

        if (RoyalFlush(cards))
            return Combination.RoyalFlush;

        else if (StraightOrStraightFlush(cards))
            return Combination.StraightFlush;

        return Combination.Flush;
    }

    Combination CheckRepeatCombination(List<Card> cards)
    {
        int repeatCount = cards.Count - CanDistinctCount(cards);

        if (repeatCount > 1)
        {
            if (FourOfAKind(cards))
                return Combination.FourOfAKind;

            else if (FullHouse(cards))
                return Combination.FullHouse;

            else if (ThreeOfAKind(cards))
                return Combination.ThreeOfAKind;

            else if (TwoPair(cards))
                return Combination.TwoPair;
        }

        return Combination.Pair;
    }

    public List<Card> ExtractCombination(List<Card> cards)
    {
        List<Card> newList = new List<Card>();

        if (cards.SortSuits().Count >= 5)
        {
            cards = cards.SortRanks();

            if (cards.Count > 5)
                RemoveExcessElements(cards);
        }

        else
        {
            cards = cards.SortRanks();

            if (Repeat(cards) == false)
                return RemoveExcessElements(cards);

            else
            {
                return ExtractRepeatCardsCombination(cards);
            }
        }

        newList = cards;
        return newList;
    }

    List<Card> ExtractRepeatCardsCombination(List<Card> cards)
    {
        Dictionary<int, Card> repeatCards = CanRepeatCards(cards);
        List<Card> newList = new List<Card>();
        newList.Capacity = 5;

        for (int i = 0; i < repeatCards.ElementAt(0).Key; i++)
        {
            newList.Add(repeatCards.ElementAt(0).Value);
        }

        for (int j = 0; j < repeatCards.ElementAt(1).Key; j++)
        {
            newList.Add(repeatCards.ElementAt(1).Value);
        }

        if (newList.Count > 5)
        {
            newList.SortRanks();
            RemoveExcessElements(newList);
        }

        return newList;
    }

    List<Card> RemoveExcessElements(List<Card> cards)
    {
        List<Card> newList = new List<Card>();
        newList.Capacity = 5;

        for (int i = 0; i < newList.Capacity; i++)
            newList[i] = cards[i];

        cards.Clear();

        return newList;
    }

    public bool Repeat(List<Card> cards) => (CanDistinctCount(cards) < cards.Count);

    public bool ComparisonForHightCard(List<Card> first, List<Card> second) => first.SortRanks()[0] > second.SortRanks()[0];

    public bool ComparisonForPairCards(List<Card> first, List<Card> second)
    {
        if (CanRepeatCards(first).ElementAt(0).Value ==
            CanRepeatCards(second).ElementAt(0).Value)
        {
            return CanRepeatCards(first).ElementAt(1).Value >
                   CanRepeatCards(second).ElementAt(1).Value ? true : false;
        }

        else
            return CanRepeatCards(first).ElementAt(0).Value >
                   CanRepeatCards(second).ElementAt(0).Value ? true : false;
    }

    public bool ComparisonForFlushOrStraight(List<Card> first, List<Card> second)
    {
        if (GetCardsSum(first) > GetCardsSum(second))
            return true;

        else if (GetCardsSum(first) == GetCardsSum(second))
        {
            for (int i = 0; i < first.Count; i++)
            {
                if (first[i] == second[i])
                    continue;

                else if (first[i] > second[i])
                    return true;
            }
        }

        return false;
    }

    bool Pair(List<Card> cards) => RepeatCardCount(cards, 1, 2);

    static bool TwoPair(List<Card> cards) => RepeatCardCount(cards, 2, 2);

    static bool ThreeOfAKind(List<Card> cards) => RepeatCardCount(cards, 2, 3);

    static bool FullHouse(List<Card> cards) => RepeatCardCount(cards, 3, 3);

    static bool FourOfAKind(List<Card> cards) => RepeatCardCount(cards, 3, 4);

    bool StraightOrStraightFlush(List<Card> cards) => (int)cards.Max(card => card.Rank) -
                                                      (int)cards.Min(card => card.Rank) == 4;

    bool RoyalFlush(List<Card> cards) => cards.Count(card => (int)card.Rank > (int)CardType.Rank.ACE ||
                                                             (int)card.Rank < (int)CardType.Rank.TEN) == 0;

    static bool RepeatCardCount(List<Card> cards, int repeatCardCount, int maxCountRepeatForOneCard)
    {
        return cards.Count - CanDistinctCount(cards) == repeatCardCount &&
               CanMaxRepeatRankCount(CanRepeatCards(cards)) == maxCountRepeatForOneCard;
    }

    int GetCardsSum(List<Card> cards)
    {
        List<Card> sortCards = cards.SortRanks();

        int sum = (int)sortCards.Select(x => x.Rank)
                                .Aggregate((x, y) => (CardType.Rank)((int)x + (int)y));

        return sum;
    }

    static int CanDistinctCount(List<Card> cards) => cards.Select(card => (int)card.Rank).Distinct().Count();

    static int CanMaxRepeatRankCount(Dictionary<int, Card> repeatCards) => repeatCards.ElementAt(0).Key;

    Card CanMaxRepeatCardRank(Dictionary<int, Card> repeatCards) => repeatCards.ElementAt(0).Value;

    static Dictionary<int, Card> CanRepeatCards(List<Card> cards) => cards.GroupBy(x => x)
                                                                   .Where(y => y.Count() > 1)
                                                                   .ToDictionary(y => y.Count(), x => x.Key);
}
