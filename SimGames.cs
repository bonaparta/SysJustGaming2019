using System.Collections.Generic;

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

        Card[] arAxis = deck.Cards.ToArray();
        List<Card> lstAxis = new List<Card>(arAxis);
        lstAxis.Sort(Comparer<Card>.Create((x, y) =>
            (x.Rank < y.Rank || (x.Rank == y.Rank && x.Suit < y.Suit)) ? 1 :
            (x.Rank > y.Rank || (x.Rank == y.Rank && x.Suit > y.Suit)) ? -1 : 0));
        arAxis = lstAxis.ToArray();

        double[][] arReport = new double[arAxis.Length][];
        for (int i = 0; i < arReport.Length; ++i)
        {
            arReport[i] = new double[arAxis.Length];
            for (int j = 0; j < arReport[i].Length; ++j)
                arReport[i][j] = 0;
        }

        for (int i = 0; i < arAxis.Length - 1; ++i)
            for (int j = i + 1; j < arAxis.Length; ++j)
            {
                InitGame(deck, players);

                Card card = deck.HaveACardUpMySleeve(arAxis[i].Rank, arAxis[i].Suit);
                players[0].AddCard(card);
                card = deck.HaveACardUpMySleeve(arAxis[j].Rank, arAxis[j].Suit);
                players[0].AddCard(card);
        
                deck.Save();
                for (int k = 0; k < players.Count; ++k)
                    players[k].Save();
        
                int nWin = 0;
                for (int k = 0; k < s_nTimesPerSim; ++k)
                {
                    deck.Load();
                    deck.Cards = DeckCreator.Shuffle(deck.Cards);
                    for (int m = 0; m < players.Count; ++m)
                        players[m].Load();
            
                    Game game = new Game(deck, players);
                    Result result = game.Play();
            
                    if (result.Winner == players[0])
                        ++nWin;
                }

                players[0].Load();
                arReport[i][j] = (double)nWin / s_nTimesPerSim;
                System.Diagnostics.Debug.WriteLine("Progress: Player: {0} Win: {1}", players[0].Display, (double)nWin / s_nTimesPerSim);
            }

        PrintReport(arAxis, arReport);
    }

    private static bool InitGame(Deck deck, List<Player> lstPlayers)
    {
        deck.Restore();

        for (int k = 0; k < lstPlayers.Count; ++k)
            lstPlayers[k].Cards.Clear();

        return true;
    }

    private static bool PrintReport(Card[] arAxis, double[][] arReport)
    {
        string strLine = "";
        for (int i = 0; i < arAxis.Length; ++i)
            strLine += "\t" + arAxis[i].Display;
        System.Diagnostics.Debug.Write(strLine);

        for (int i = 0; i < arAxis.Length; ++i)
        {
            strLine = "\n" + arAxis[i].Display;
            for (int j = 0; j < arReport[i].Length; ++j)
                strLine += "\t" + arReport[i][j].ToString();
            System.Diagnostics.Debug.Write(strLine);
        }
        return true;
    }
}
}
