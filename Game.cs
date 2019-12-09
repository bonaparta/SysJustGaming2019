using System;
using System.Collections.Generic;

namespace PokerGame
{
public class Game
{
    private Deck m_dkDeck;
    private List<Player> m_lstPlayers;

    public Game(Deck deck, List<Player> lstPlayers)
    {
        m_dkDeck = deck;
        m_lstPlayers = lstPlayers;
    }

    public Result Play()
    {
        foreach(Player player in m_lstPlayers)
        {
            if (null == player.Hand)
                player.Hand = new Hand();
            if (null == player.Cards)
                player.Cards = new List<Card>();

            while (player.Cards.Count < 5)
                player.Cards.Add(m_dkDeck.Draw());

            player.Hand = HandCreator.Arrange(player.Cards);
        }

        return new Result
        {
            Winner = Judge.Battle(m_lstPlayers),
            Players = m_lstPlayers
        };
    }
}
}  // namespace PokerGame
