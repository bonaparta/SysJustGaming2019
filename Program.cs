using System;
using System.Collections.Generic;
using PokerGame;

namespace SysJustGaming2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            Deck deck = DeckCreator.CreateCards();
            List<Player> players = new List<Player>();
            players.Add(new Player("A"));
            players.Add(new Player("B"));
            Game game = new Game(deck, players);

            Result result = game.Play();

            Console.WriteLine("Winer: {0} Hand: {1}", result.Winner.Name, result.Winner.Display);
        }
    }
}  // namespace SysJustGaming2019
