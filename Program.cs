using System;
using System.Collections.Generic;
using PokerGame;

namespace SysJustGaming2019
{
    class Program
    {
        static void Main(string[] args)
        {
            PokerGame.Simulator.SimGames.SimTwoCardsInit();
            //PokerGame.Simulator.SimGamesFini.SimFiveCardsFini();
            //PokerGame.Simulator.SimGamesDistribution.SimDistribution();

            Console.WriteLine("Hello World");
            Deck deck = DeckCreator.CreateCards();
            List<Player> players = new List<Player>();
            players.Add(new Player("P1"));
            players.Add(new Player("P2"));
            players.Add(new Player("P3"));
            players.Add(new Player("P4"));
            Game game = new Game(deck, players);

            Result result = game.Play();

            //System.Diagnostics.Debug.WriteLine("Winer: {0} Hand: {1}", result.Winner.Name, result.Winner.Display);
            Console.WriteLine("Winer: {0} Hand: {1}", result.Winner.Name, result.Winner.Display);
        }
    }
}  // namespace SysJustGaming2019
