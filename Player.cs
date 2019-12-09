using System.Collections.Generic;

namespace PokerGame
{
public class Player
{
    public string Name { get; set; }
    public List<Card> Cards { get; set; }
    public Hand Hand { get; set; }

    public Player() { }

    public Player(string name)
    {
        Name = name;
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
        Hand = null;
    }

    public string Display
    {
        get
        {
            if (null == Hand)
                Hand = HandCreator.Arrange(Cards);

            string strReturn = System.Enum.GetName(typeof(HandRank), Hand.HandRank);
            foreach (Card card in Hand.SortedCards)
            {
                strReturn += " " + card.DisplayCard;
            }
            return strReturn;
        }
    }

    public void Save()
    {
        m_arFile = Cards.ToArray();
    }

    public void Load()
    {
        if (null != m_arFile)
        {
            List<Card> lstLoad = new List<Card>(m_arFile);
            Cards = lstLoad;
            Hand = null;
        }
    }

    private Card[] m_arFile;
}
}  // namespace PokerGame
