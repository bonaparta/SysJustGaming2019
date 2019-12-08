namespace PokerGame
{
public class Card
{
    public string Display { get; set; }
    public Suit Suit { get; set; }
    public int Rank { get; set; }
    public string DisplayCard
    {
        get
        {
            return GetShortName(Suit, Rank);
        }
    }

    private static string GetShortName(Suit suit, int value)
    {
        string strSuit = "";
        if (Suit.Spades == suit)
            strSuit = "S";
        else if (Suit.Hearts == suit)
            strSuit = "H";
        else if (Suit.Diamonds == suit)
            strSuit= "D";
        else if (Suit.Clubs == suit)
            strSuit = "C";

        string strValue = "";
        if (value >= 2 && value <= 10)
        {
            strValue = value.ToString();
        }
        else if (value == 11)
        {
            strValue = "J";
        }
        else if (value == 12)
        {
            strValue = "Q";
        }
        else if (value == 13)
        {
            strValue = "K";
        }
        else if (value == 14)
        {
            strValue = "A";
        }

        return strSuit + strValue;
    }
    }
}  // namespace PokerGames
