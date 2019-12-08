using System.Collections.Generic;

namespace PokerGame
{
public class Deck
{
    public Card Draw()
    {
        if (m_qCards.Count > 0)
            return m_qCards.Dequeue();
        return null;
    }

    public Card HaveACardUpMySleeve(int nRank, Suit eSuit)
    {
        Queue<Card> tmpQ = new Queue<Card>();
        while (m_qCards.Count > 0)
        {
            Card card = m_qCards.Dequeue();
            if (nRank == card.Rank && eSuit == card.Suit)
            {
                while (m_qCards.Count > 0)
                    tmpQ.Enqueue(m_qCards.Dequeue());
                return card;
            }
            else
                tmpQ.Enqueue(card);
        }
        return null;
    }

    public void Save()
    {
        m_arFile = m_qCards.ToArray();
    }

    public void Load()
    {
        if (null != m_arFile)
        {
            Queue<Card> qLoad = new Queue<Card>(m_arFile);
            m_qCards = qLoad;
        }
    }

    private Queue<Card> m_qCards;
    private Card[] m_arFile;
}
}  // namespace PokerGame
