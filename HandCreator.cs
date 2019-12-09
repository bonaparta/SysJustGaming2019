using System.Collections.Generic;

namespace PokerGame
{
class HandCreator
{
    public static Hand Arrange(List<Card> cards)
    {
        System.Tuple<bool, Hand> pairHand = CheckStraightFlush(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckFourOfAKind(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckFullHouse(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckFlush(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckStraight(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckThreeOfAKind(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckTwoPair(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        pairHand = CheckOnePair(cards);
        if (pairHand.Item1)
        {
            return pairHand.Item2;
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.HighCard;
        List<Card> arCards = new List<Card>();
        Card[] arTmpCards = cards.ToArray();
        foreach (Card card in arTmpCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) =>
            (x.Rank > y.Rank || (x.Rank == y.Rank && x.Suit > y.Suit)) ? 1 :
            (x.Rank < y.Rank || (x.Rank == y.Rank && x.Suit < y.Suit)) ? -1 : 0));
        hand.SortedCards = arCards;
        return hand;
    }

    private static System.Tuple<bool, Hand> CheckStraightFlush(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        Suit nDefaultSuit = cards[0].Suit;
        foreach (Card card in cards)
        {
            int nIndex = card.Rank - (int)CardValue.MinValue;
            if (nDefaultSuit == card.Suit && !arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return System.Tuple.Create<bool, Hand>(false, null);
        }

        // 特規 6 張牌
        if (!(arLinearSort[0] ^ arLinearSort[(int)CardValue.MaxValue - (int)CardValue.MinValue]))
            return System.Tuple.Create<bool, Hand>(false, null);;

        Hand hand = new Hand();
        hand.HandRank = HandRank.StraightFlush;
        List<Card> arCards = new List<Card>();
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Rank > y.Rank ? 1 : x.Rank < y.Rank ? -1 : 0));
        hand.SortedCards = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFourOfAKind(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card>[] arLinearSort = new List<Card>[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new List<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Rank - (int)CardValue.MinValue;
            arLinearSort[nIndex].Add(card);
            nCount = System.Math.Max(nCount, arLinearSort[nIndex].Count);
        }
        if (Hand.s_nFourOfAKind != nCount)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FourOfAKind;
        List<Card> arCards = new List<Card>();

        for (int i = 0; i < arLinearSort.Length; ++i)
        {
            if (Hand.s_nFourOfAKind == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
                arCards.AddRange(arLinearSort[i]);
                for (int j = 0; j < arLinearSort.Length; ++j)
                {
                    if (Hand.s_nFourOfAKind != arLinearSort[j].Count)
                    {
                        arCards.AddRange(arLinearSort[j]);
                        break;
                    }
                }
                break;
            }
        }
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFullHouse(List<Card> cards)
    {
        System.Tuple<bool, Hand> prThreeOfAKindHand = CheckThreeOfAKind(cards);
        if (!prThreeOfAKindHand.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card> qPairCadidate = new List<Card>();
        List<Card> lstCards = prThreeOfAKindHand.Item2.SortedCards.GetRange(Hand.s_nThreeOfAKind, prThreeOfAKindHand.Item2.SortedCards.Count - Hand.s_nThreeOfAKind);
        foreach (Card card in lstCards)
            qPairCadidate.Add(card);

        System.Tuple<bool, Hand> prPairHand = CheckOnePair(qPairCadidate);
        if (!prPairHand.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FullHouse;
        List<Card> arCards = new List<Card>();
        arCards.AddRange(prThreeOfAKindHand.Item2.SortedCards.GetRange(0, Hand.s_nThreeOfAKind));
        arCards.AddRange(prPairHand.Item2.SortedCards);
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFlush(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        Suit nDefaultSuit = cards[0].Suit;
        foreach (Card card in cards)
        {
            if (nDefaultSuit != card.Suit)
                return System.Tuple.Create<bool, Hand>(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.Flush;
        List<Card> arCards = new List<Card>();
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Rank > y.Rank ? 1 : x.Rank < y.Rank ? -1 : 0));
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckStraight(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        foreach (Card card in cards)
        {
            int nIndex = card.Rank - (int)CardValue.MinValue;
            if (!arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return System.Tuple.Create<bool, Hand>(false, null);
        }

        // 特規 6 張牌
        if (!(arLinearSort[0] ^ arLinearSort[(int)CardValue.MaxValue - (int)CardValue.MinValue]))
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.Straight;
        List<Card> arCards = new List<Card>();
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Rank > y.Rank ? 1 : x.Rank < y.Rank ? -1 : 0));
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckThreeOfAKind(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card>[] arLinearSort = new List<Card>[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new List<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Rank - (int)CardValue.MinValue;
            arLinearSort[nIndex].Add(card);
            nCount = System.Math.Max(nCount, arLinearSort[nIndex].Count);
        }
        if (Hand.s_nThreeOfAKind != nCount)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.ThreeOfAKind;
        List<Card> arCards = new List<Card>();
        
        for (int i = 0; i < arLinearSort.Length; ++i)
        {
            if (Hand.s_nThreeOfAKind == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
                arCards.AddRange(arLinearSort[i]);
                for (int j = arLinearSort.Length - 1; j >= 0; --j)
                {
                    if (Hand.s_nThreeOfAKind != arLinearSort[j].Count)
                    {
                        arCards.AddRange(arLinearSort[j]);
                    }
                }
                break;
            }
        }
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckTwoPair(List<Card> cards)
    {
        System.Tuple<bool, Hand> prOnePairHand = CheckOnePair(cards);
        if (!prOnePairHand.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card> qPairCadidate = new List<Card>();
        List<Card> lstCards = prOnePairHand.Item2.SortedCards.GetRange(Hand.s_nPair, prOnePairHand.Item2.SortedCards.Count - Hand.s_nPair);
        foreach (Card card in lstCards)
            qPairCadidate.Add(card);

        System.Tuple<bool, Hand> prPairHand = CheckOnePair(qPairCadidate);
        if (!prPairHand.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.TwoPair;
        List<Card> arCards = new List<Card>();
        arCards.AddRange(prOnePairHand.Item2.SortedCards.GetRange(0, Hand.s_nPair));
        arCards.AddRange(prPairHand.Item2.SortedCards);
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckOnePair(List<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card>[] arLinearSort = new List<Card>[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new List<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Rank - (int)CardValue.MinValue;
            arLinearSort[nIndex].Add(card);
            nCount = System.Math.Max(nCount, arLinearSort[nIndex].Count);
        }
        if (Hand.s_nPair != nCount)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.Pair;
        List<Card> arCards = new List<Card>();

        for (int i = arLinearSort.Length - 1; i >= 0 ; --i)
        {
            if (Hand.s_nPair == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
                arCards.AddRange(arLinearSort[i]);
                for (int j = arLinearSort.Length - 1; j >= 0; --j)
                {
                    if (i != j)
                    {
                        arCards.AddRange(arLinearSort[j]);
                        break;
                    }
                }
                break;
            }
        }
        hand.SortedCards = arCards;
        return System.Tuple.Create<bool, Hand>(true, hand);
    }
}
}  // namespace PokerGame
