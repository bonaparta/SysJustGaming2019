class HandCreator
{
    Hand Arrange(Queue<Card> cards)
    {
        Pair<bool, Hand> pairHand = CheckStraightFlush(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckFourOfAKind(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckFullHouse(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckFlush(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckStraight(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckThreeOfAKind(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckTwoPair(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        pairHand = CheckOnePair(cards);
        if (pairHand.First)
        {
            return pairHand.Second;
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.HighCard;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) =>
            (x.Value > y.Value || (x.Value == y.Value && x.Suit > y.Suit)) ?  1 :
            (x.Value < y.Value || (x.Value == y.Value && x.Suit < y.Suit)) ?  -1 : 0);
        hand.Deck = arCards;
        return hand;
    }

    private static Pair<bool, Hand?> CheckStraightFlush(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return Pair.Create(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        Suit nDefaultSuit = cards.Peek().Suit;
        foreach (Card card in cards)
        {
            if (nDefaultSuit == card.Suit && !arLinearSort[card.Value - (int)CardValue.MinValue])
                arLinearSort[i] = true;
            else
                return Pair.Create(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.StraightFlush;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ?  1 : x.Value < y.Value ?  -1 : 0);
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckFourOfAKind(Queue<Card> cards) {
        return true;
    }

    private static Pair<bool, Hand?> CheckFullHouse(Queue<Card> cards) {
        return true;
    }

    private static Pair<bool, Hand?> CheckFlush(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return Pair.Create(false, null);

        Suit nDefaultSuit = cards.Peek().Suit;
        foreach (Card card in cards)
        {
            if (nDefaultSuit != card.Suit)
                return Pair.Create(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.Flush;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ?  1 : x.Value < y.Value ?  -1 : 0);
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckStraight(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return Pair.Create(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        foreach (Card card in cards)
        {
            if (!arLinearSort[card.Value - (int)CardValue.MinValue])
                arLinearSort[i] = true;
            else
                return Pair.Create(false, null);
        }

        // 特規 6 張牌
        if (arLinearSort[0] && arLinearSort[(int)CardValue.MaxValue - (int)CardValue.MinValue])
            return Pair.Create(false, null);;

        Hand hand = new Hand();
        hand.HandRank = HandRank.Straight;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ?  1 : x.Value < y.Value ?  -1 : 0);
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckThreeOfAKind(Queue<Card> cards) {
        return true;
    }

    private static Pair<bool, Hand?> CheckTwoPair(Queue<Card> cards) {
        return true;
    }

    private static Pair<bool, Hand?> CheckOnePair(Queue<Card> cards) {
        return true;
    }
}
