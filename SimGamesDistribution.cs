using System.Collections.Generic;

namespace PokerGame.Simulator
{
public class SimGamesDistribution
{
    private static int s_nTimesPerSim = 1000000;

    public static void SimDistribution()
    {
        Deck deck = DeckCreator.CreateCards();
        List<Player> players = new List<Player>();
        for (int i = 0; i < 1; ++i)
        {
            players.Add(new Player("P" + i.ToString()));
            players[i].Cards = new List<Card>();
        }

        for (int i = 0; i < s_nTimesPerSim; ++i)
        {
            InitGame(deck, players);
            deck.Cards = DeckCreator.Shuffle(deck.Cards);
            Play(deck, players[0]);

            if (i % 10000 == 0)
                System.Diagnostics.Debug.WriteLine("Progress: Player: {0} Time: {1} Hand: {2}", players[0].Display, i, players[0].Cards[0].DisplayCard);

            if (HandRank.StraightFlush == players[0].Hand.HandRank)
            {
                System.Diagnostics.Debug.WriteLine("End: Player: {0} Time: {1} Hand: {2}", players[0].Display, i, players[0].Hand.HandRank);
                break;
            }
        }
    }

    private static bool Play(Deck deck, Player player)
    {
        while (player.Cards.Count < 5)
            player.Cards.Add(deck.Draw());

        player.Hand = HandCreator.Arrange(player.Cards);
        return true;
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
