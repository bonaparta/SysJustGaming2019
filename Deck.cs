using System.Collections.Generic;

namespace PokerGame
{
public class Deck
{
    public Queue<Card> Cards { get; set; }
    private Card[] m_arFile;
    private Card[] m_arOriginCards;

    public Deck(Queue<Card> arCards)
    {
        Cards = arCards;
        m_arOriginCards = arCards.ToArray();
    }

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

    public void Restore()
    {
        if (null != m_arOriginCards)
        {
            Queue<Card> qLoad = new Queue<Card>(m_arOriginCards);
            Cards = qLoad;
        }
    }
}
}  // namespace PokerGame
