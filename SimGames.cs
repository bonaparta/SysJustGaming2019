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

        //foreach (IEnumerable<Card> cards in Combinations(deck.Cards, 2))
        //{
            
        //}

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
            deck.Cards = DeckCreator.Shuffle(deck.Cards);
            for (int j = 0; j < players.Count; ++j)
                players[j].Load();
            
            Game game = new Game(deck, players);
            Result result = game.Play();
            
            if (result.Winner == players[0])
                ++nWin;
        }

        players[0].Load();
        System.Diagnostics.Debug.WriteLine("Player: {0} Win: {1}", players[0].Display, (double)nWin / s_nTimesPerSim);
    }

    //private static bool NextCombination(IList<int> num, int n, int k)
    //  {
    //     bool finished;
 
    //     var changed = finished = false;
 
    //     if (k <= 0) return false;
 
    //     for (var i = k - 1; !finished && !changed; i--)
    //     {
    //        if (num[i] < n - 1 - (k - 1) + i)
    //        {
    //           num[i]++;
 
    //           if (i < k - 1)
    //              for (var j = i + 1; j < k; j++)
    //                 num[j] = num[j - 1] + 1;
    //           changed = true;
    //        }
    //        finished = i == 0;
    //     }
 
    //     return changed;
    //  }
 
    //  private static IEnumerable<T> Combinations<T>(IEnumerable<T> elements, int k)
    //  {
    //     var elem = elements.ToArray();
    //     var size = elem.Length;
 
    //     if (k > size) yield break;
 
    //     var numbers = new int[k];
 
    //     for (var i = 0; i < k; i++)
    //        numbers[i] = i;
 
    //     do
    //     {
    //        yield return numbers.Select(n => elem[n]);
    //     } while (NextCombination(numbers, size, k));
    //  }
}
}
