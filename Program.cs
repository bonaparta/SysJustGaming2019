using System;
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
                Value = 1,
                DisplayName = "S" + 10
            };
            Console.WriteLine(card.ToString());
            Console.WriteLine(card.DisplayName);
        }
    }
}  // namespace SysJustGaming2019
