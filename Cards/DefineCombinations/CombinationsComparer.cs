using System.Collections.Generic;
using System.Linq;

public class CombinationsComparer
{
    private CardCombinationsDefiner _definer;
    private Dictionary<Player, Combination> _playersCombinations =
        new Dictionary<Player, Combination>();

    public CombinationsComparer(CardCombinationsDefiner definer)
    {
        _definer = definer;
    }

    public Player CompareCombinations(List<Player> players, CardsOnTable cardsOnTable)
    {
        DefineCombinations(players, cardsOnTable);

        Dictionary<Player, Combination> sortCombinations = _playersCombinations.OrderByDescending(x => x.Value)
                                                                                .ToDictionary(y => y.Key, x => x.Value);

        if (sortCombinations.Distinct().Count() < sortCombinations.Count)
        {
            Player playerCards1 = sortCombinations.ElementAt(0).Key;
            Player playerCards2 = sortCombinations.ElementAt(1).Key;
            Combination combination = sortCombinations.ElementAt(0).Value;

            return CompareSameCombinations(playerCards1, playerCards2, combination, cardsOnTable);
        }

        else return sortCombinations.ElementAt(0).Key;
    }

    private void DefineCombinations(List<Player> players, CardsOnTable cardsOnTable)
    {
        foreach (var player in players)
        {
            List<Card> uniteCards = GetUniteCards(player.Cards.Cards.ToList(), cardsOnTable.Cards);
            Combination combination = _definer.CheckCombination(uniteCards);

            _playersCombinations.Add(player, combination);
        }
    }

    private Player CompareSameCombinations(Player first, Player second, Combination combination, CardsOnTable cardsOnTable)
    {
        List<Card> cards1 = GetUniteCards(first.Cards.Cards.ToList(),
                                          cardsOnTable.Cards);
        List<Card> cards2 = GetUniteCards(second.Cards.Cards.ToList(),
                                          cardsOnTable.Cards);

        cards1 = _definer.ExtractCombination(cards1);
        cards2 = _definer.ExtractCombination(cards2);

        if (_definer.Repeat(cards1))
            return _definer.ComparisonForPairCards(cards1, cards2) ? first : second;

        else if (combination != Combination.Pair)
            return _definer.ComparisonForFlushOrStraight(cards1, cards2) ? first : second;

        else
            return _definer.ComparisonForHightCard(cards1, cards2) ? first : second;
    }

    private List<Card> GetUniteCards(List<Card> handCards, List<Card> tableCards) => handCards.Concat(tableCards).ToList();
}

