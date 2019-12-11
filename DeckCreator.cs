using System;
using System.Collections.Generic;

namespace PokerGame
{
public static class DeckCreator
{
    public static Deck CreateCards()
    {
        Queue<Card> cards = new Queue<Card>();
        for (int i = (int)CardValue.MinValue; i <= (int)CardValue.MaxValue; i++)
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                cards.Enqueue(new Card()
                {
                    Suit = suit,
                    Rank = i,
                    Display = GetShortName(i, suit)
                });
            }
        }

        cards = Shuffle(cards);
        Deck deck = new Deck(cards);
        return deck;
    }

    public static Queue<Card> Shuffle(Queue<Card> cards)
    {
        //Shuffle the existing cards using Fisher-Yates Modern
        Card[] transformedCards = cards.ToArray();
        Random r = new Random(Guid.NewGuid().GetHashCode());
        for (int n = transformedCards.Length - 1; n > 0; --n)
        {
            //Step 2: Randomly pick a card which has not been shuffled
            int k = r.Next(n + 1);

            //Step 3: Swap the selected item with the last "unselected" card in the collection
            Card temp = transformedCards[n];
            transformedCards[n] = transformedCards[k];
            transformedCards[k] = temp;
        }

        Queue<Card> shuffledCards = new Queue<Card>();
        foreach (var card in transformedCards)
        {
            shuffledCards.Enqueue(card);
        }

        return shuffledCards;
    }

    private static string GetShortName(int value, Suit suit)
    {
        string valueDisplay = "";
        if (value >= 2 && value < 10)
        {
            valueDisplay = value.ToString();
        }
        else if (value == 10)
        {
            valueDisplay = "T";
        }
        else if (value == 11)
        {
            valueDisplay = "J";
        }
        else if (value == 12)
        {
            valueDisplay = "Q";
        }
        else if (value == 13)
        {
            valueDisplay = "K";
        }
        else if (value == 14)
        {
            valueDisplay = "A";
        }

        return valueDisplay + Enum.GetName(typeof(Suit), suit)[0];
    }
}
}  // namespace PokerGame
