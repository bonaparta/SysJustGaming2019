class Hand
{
    public HandRank HandRank { get; set; }
    public List<Card> Deck { get; set; }

    public static int s_nFourOfAKind = 4;
    public static int s_nThreeOfAKind = 3;
    public static int s_nPair = 2;
}
