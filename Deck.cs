using System.Collections.Generic;

namespace PokerGame
{
public class Deck
{
    public Queue<Card> Cards { get; set; }
    private Card[] m_arFile;

    public Card Draw()
    {
        if (Cards.Count > 0)
            return Cards.Dequeue();
        return null;
    }

    public Card HaveACardUpMySleeve(int nRank, Suit eSuit)
    {
        Queue<Card> tmpQ = new Queue<Card>();
        while (Cards.Count > 0)
        {
            Card card = Cards.Dequeue();
            if (nRank == card.Rank && eSuit == card.Suit)
            {
                while (Cards.Count > 0)
                    tmpQ.Enqueue(Cards.Dequeue());
                Cards = tmpQ;
                return card;
            }
            else
                tmpQ.Enqueue(card);
        }
        return null;
    }

    public void Save()
    {
        m_arFile = Cards.ToArray();
    }

    public void Load()
    {
        if (null != m_arFile)
        {
            Queue<Card> qLoad = new Queue<Card>(m_arFile);
            Cards = qLoad;
        }
    }
}
}  // namespace PokerGame
