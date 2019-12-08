using System;
using System.Collections.Generic;

namespace PokerGame
{
public class Game
{
    private List<Player> m_lstPlayers;
    private Deck m_dkDeck;
    public Game(List<Player> lstPlayers, Deck dkDeck)
    {
        m_lstPlayers = lstPlayers;
        m_dkDeck = dkDeck;
    }

    public Result Play()
    {
        foreach(Player player in m_lstPlayers)
        {
            if (null == player.Hand)
                player.Hand = new Hand();
            if (null == player.Hand.Cards)
                player.Hand.Cards = new List<Card>();

            while (player.Hand.Cards.Count < 5)
                player.Hand.Cards.Add(m_dkDeck.Draw());
        }

        return new Result
        {
            Winner = Judge.Battle(m_lstPlayers),
            Players = m_lstPlayers
        };
    }
}
}  // namespace PokerGame
