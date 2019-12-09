using System.Collections.Generic;
using PokerGame;

namespace PokerGame.Simulator
{
public class SimGames
{
    private static int s_nTimesPerSim = 1000000;

    public static void SimTwoCardsInit()
    {
        Deck deck = DeckCreator.CreateCards();
        List<Player> players = new List<Player>();
        for (int i = 0; i < 4; ++i)
        {
            players.Add(new Player("P" + i.ToString()));
            players[i].Cards = new List<Card>();
        }

        Card card = deck.HaveACardUpMySleeve(14, Suit.Spades);
        players[0].AddCard(card);
        card = deck.HaveACardUpMySleeve(14, Suit.Hearts);
        players[0].AddCard(card);
        
        deck.Save();
        for (int i = 0; i < players.Count; ++i)
            players[i].Save();
        
        int nWin = 0;
        for (int i = 0; i < s_nTimesPerSim; ++i)
        {
            deck.Load();
            for (int j = 0; j < players.Count; ++j)
                players[i].Load();
            
            Game game = new Game(deck, players);
            Result result = game.Play();
            
            if (result.Winner == players[0])
                ++nWin;
        }

        players[0].Load();
        System.Diagnostics.Debug.WriteLine("Player: {0} Win: {1}", players[0].Display, (double)nWin / s_nTimesPerSim);
    }
}
}
