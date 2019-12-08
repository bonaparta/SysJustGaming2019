using System.Collections.Generic;

namespace PokerGame
{
public class Hand
{
    public HandRank HandRank { get; set; }
    public List<Card> Cards { get; set; }
    public List<Card> SortedCards { get; set; }

    public static int s_nFourOfAKind = 4;
    public static int s_nThreeOfAKind = 3;
    public static int s_nPair = 2;
}
}  // namespace PokerGame
