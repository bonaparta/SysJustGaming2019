using System.Collections.Generic;

namespace PokerGame
{
public class Player
{
    public string Name { get; set; }
    public Queue<Card> Deck { get; set; }

    public Player() { }

    public Player(string name)
    {
        Name = name;
    }

    public Queue<Card> Deal(Queue<Card> cards)
    {
        Queue<Card> player1cards = new Queue<Card>();
        Queue<Card> player2cards = new Queue<Card>();

        int counter = 2;
        while (cards.Count > 0)
        {
            if (counter % 2 == 0) //Card etiquette says the player who is NOT the dealer gets first card
            {
                player2cards.Enqueue(cards.Dequeue());
            }
            else
            {
                player1cards.Enqueue(cards.Dequeue());
            }
            counter++;
        }

        Deck = player1cards;
        return player2cards;
    }
}
}  // namespace PokerGame
