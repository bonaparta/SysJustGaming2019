﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysJustGaming2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            PokerGame.Card card = new PokerGame.Card()
            {
                Suit = PokerGame.Suit.Clubs,
                Rank = 1,
                Display = "S" + 10
            };
            Console.WriteLine(card.ToString());
            Console.WriteLine(card.Display);
        }
    }
}  // namespace SysJustGaming2019
