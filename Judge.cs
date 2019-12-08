using System.Collections;
using System.Collections.Generic;

namespace PokerGame
{
public class Judge
{
    public static Player Battle(List<Player> lstPlayers)
    {
        if (null == lstPlayers || 0 == lstPlayers.Count)
            return null;
        
        Player plrWinner = null;
        foreach (Player player in lstPlayers)
        {
            if (null == plrWinner)
            {
                plrWinner = player;
                continue;
            }

            if (plrWinner.Hand.HandRank.CompareTo(player.Hand.HandRank) < 0)
            {
                plrWinner = player;
                continue;
            }

            if (plrWinner.Hand.HandRank.CompareTo(player.Hand.HandRank) == 0)
            {
                if (plrWinner.Hand.SortedCards[0].Rank < player.Hand.SortedCards[0].Rank)
                {
                    plrWinner = player;
                    continue;
                }

                if (plrWinner.Hand.SortedCards[0].Rank == player.Hand.SortedCards[0].Rank)
                {
                    if (plrWinner.Hand.SortedCards[0].Suit.CompareTo(player.Hand.SortedCards[0].Suit) < 0)
                    {
                        plrWinner = player;
                        continue;
                    }
                }
            }
        }
        return plrWinner;
    }
}
}  // namespace PokerGame
