using System.Collections.Generic;

namespace PokerGame.Simulator
{
public class SimGamesFini
{
    private static int s_nTimesPerSim = 1000000;

    public static void SimFiveCardsFini()
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

        string[] arHandRankNames = System.Enum.GetNames(typeof(HandRank));

        double[][] arReport = new double[arHandRankNames.Length][];
        for (int i = 0; i < arHandRankNames.Length; ++i)
        {
            arReport[i] = new double[arAxis.Length];
            for (int j = 0; j < arReport[i].Length; ++j)
                arReport[i][j] = 0;
        }

        for (int i = 0; i < s_nTimesPerSim; ++i)
        {
            InitGame(deck, players);
            deck.Cards = DeckCreator.Shuffle(deck.Cards);
            Game game = new Game(deck, players);
            Result result = game.Play();
            int nAxis = System.Array.IndexOf(arAxis, result.Winner.Hand.SortedCards[0]);
            ++arReport[(int)result.Winner.Hand.HandRank][nAxis];

            if (i % 1000000 == 0)
                System.Diagnostics.Debug.WriteLine("Progress: Player: {0} Time: {1}", players[0].Display, i);
        }

        for (int i = 0; i < arReport.Length; ++i)
            for (int j = 0; j < arReport[i].Length; ++j)
                arReport[i][j] /= s_nTimesPerSim;

        PrintReport(arHandRankNames, arAxis, arReport);
    }

    private static bool InitGame(Deck deck, List<Player> lstPlayers)
    {
        deck.Restore();

        for (int k = 0; k < lstPlayers.Count; ++k)
            lstPlayers[k].Cards.Clear();

        return true;
    }

    private static bool PrintReport(string[] arRanks, Card[] arAxis, double[][] arReport)
    {
        for (int i = 0; i < arRanks.Length; ++i)
            System.Diagnostics.Debug.Write("\t", arRanks[i]);
        System.Diagnostics.Debug.WriteLine("");

        for (int i = 0; i < arAxis.Length; ++i)
        {
            System.Diagnostics.Debug.Write(arAxis[i].DisplayCard);
            for (int j = 0; j < arRanks.Length; ++j)
                System.Diagnostics.Debug.Write("\t", arReport[j][i].ToString());
            System.Diagnostics.Debug.WriteLine("");
        }
        return true;
    }
}
}
