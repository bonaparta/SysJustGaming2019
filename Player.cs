using System.Collections.Generic;

namespace PokerGame
{
public class Player
{
    public string Name { get; set; }
    public Hand Hand { get; set; }

    public Player() { }

    public Player(string name)
    {
        Name = name;
    }

    public void AddCard(Card card)
    {
        Hand.Cards.Add(card);
    }

    public string Display
    {
        get
        {
            if (null == Hand.SortedCards)
                Hand.SortedCards = HandCreator.Arrange(Hand.Cards);

            string strReturn = System.Enum.GetName(typeof(HandRank), Hand.HandRank);
            foreach (Card card in Hand.Cards)
            {
                strReturn += " " + card.Display;
            }
            return strReturn;
        }
    }

    public void Save()
    {
        m_arFile = Hand.Cards.ToArray();
    }

    public void Load()
    {
        if (null != m_arFile)
        {
            List<Card> lstLoad = new List<Card>(m_arFile);
            Hand.Cards = lstLoad;
            Hand.SortedCards = null;
        }
    }

    private Card[] m_arFile;
}
}  // namespace PokerGame
