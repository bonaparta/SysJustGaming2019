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
            (x.Value > y.Value || (x.Value == y.Value && x.Suit > y.Suit)) ? 1 :
            (x.Value < y.Value || (x.Value == y.Value && x.Suit < y.Suit)) ? -1 : 0);
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
            int nIndex = card.Value - (int)CardValue.MinValue;
            if (nDefaultSuit == card.Suit && !arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return Pair.Create(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.StraightFlush;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0);
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckFourOfAKind(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return Pair.Create(false, null);

        List<Card>[] arLinearSort = new ArrayList[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new ArrayList<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
            arLinearSort[nIndex].Add(card);
            nCount = Math.Max(nCount, arLinearSort[nIndex].Count);
        }
        if (Hand.s_nFourOfAKind != nCount)
            return Pair.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FourOfAKind;
        ArrayList<Card> arCards = new ArrayList<Card>();

        for (int i = 0; i < arLinearSort.Count; ++i)
        {
            if (HandRank.FourOfAKind == arLinearSort[i])
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0);
                arCards.Add(arLinearSort[i]);
                for (int j = 0; j < arLinearSort.Count; ++j)
                {
                    if (HandRank.FourOfAKind != arLinearSort[j])
                    {
                        arCards.Add(arLinearSort[j]);
                        break;
                    }
                }
                break;
            }
        }
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckFullHouse(Queue<Card> cards)
    {
        Pair<bool, Hand?> prThreeOfAKindHand = CheckThreeOfAKind(cards);
        if (!prThreeOfAKindHand.First)
            return Pair.Create(false, null);

        Queue<Card> qPairCadidate = new Queue<Card>();
        qPairCadidate.Add(prThreeOfAKindHand.Second.Deck.GetRange(Hand.s_nThreeOfAKind, prThreeOfAKindHand.Second.Deck.Count - Hand.s_nThreeOfAKind));

        Pair<bool, Hand?> prPair = CheckPair(qPairCadidate);
        if (!qPairCadidate.First)
            return Pair.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FourOfAKind;
        ArrayList<Card> arCards = new ArrayList<Card>();

        hand.Deck = new ArrayList<Card>();
        hand.Deck.Add(prThreeOfAKindHand.Second.Deck.GetRange(0, Hand.s_nThreeOfAKind);
        hand.Deck.Add(qPairCadidate.Second.Deck);
        return Pair.Create(true, hand);
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
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0);
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
            int nIndex = card.Value - (int)CardValue.MinValue;
            if (!arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
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
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0);
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckThreeOfAKind(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return Pair.Create(false, null);

        List<Card>[] arLinearSort = new ArrayList[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new ArrayList<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
            arLinearSort[nIndex].Add(card);
            nCount = Math.Max(nCount, arLinearSort[nIndex].Count);
        }
        if (Hand.s_nThreeOfAKind != nCount)
            return Pair.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.ThreeOfAKind;
        ArrayList<Card> arCards = new ArrayList<Card>();
        
        for (int i = 0; i < arLinearSort.Count; ++i)
        {
            if (HandRank.ThreeOfAKind == arLinearSort[i])
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0);
                arCards.Add(arLinearSort[i]);
                for (int j = 0; j < arLinearSort.Count; ++j)
                {
                    if (HandRank.ThreeOfAKind != arLinearSort[j])
                    {
                        arCards.Add(arLinearSort[j]);
                        break;
                    }
                }
                break;
            }
        }
        return Pair.Create(true, hand);
    }

    private static Pair<bool, Hand?> CheckTwoPair(Queue<Card> cards) {
        return true;
    }

    private static Pair<bool, Hand?> CheckOnePair(Queue<Card> cards) {
        return true;
    }
}
