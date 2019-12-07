using System.Collections.Generic;

namespace PokerGame
{
class HandCreator
{
    public static Hand Arrange(Queue<Card> cards)
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
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) =>
            (x.Value > y.Value || (x.Value == y.Value && x.Suit > y.Suit)) ? 1 :
            (x.Value < y.Value || (x.Value == y.Value && x.Suit < y.Suit)) ? -1 : 0));
        hand.Deck = arCards;
        return hand;
    }

    private static System.Tuple<bool, Hand> CheckStraightFlush(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        Suit nDefaultSuit = ((Card)cards.Peek()).Suit;
        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
            if (nDefaultSuit == card.Suit && !arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return System.Tuple.Create<bool, Hand>(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.StraightFlush;
        List<Card> arCards = new List<Card>();
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFourOfAKind(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card>[] arLinearSort = new List<Card>[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new List<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
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
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFullHouse(Queue<Card> cards)
    {
        System.Tuple<bool, Hand> prThreeOfAKindHand = CheckThreeOfAKind(cards);
        if (!prThreeOfAKindHand.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        Queue<Card> qPairCadidate = new Queue<Card>();
        List<Card> lstCards = prThreeOfAKindHand.Item2.Deck.GetRange(Hand.s_nThreeOfAKind, prThreeOfAKindHand.Item2.Deck.Count - Hand.s_nThreeOfAKind);
        foreach (Card card in lstCards)
            qPairCadidate.Enqueue(card);

        System.Tuple<bool, Hand> prPair = CheckOnePair(qPairCadidate);
        if (!prPair.Item1)
            return System.Tuple.Create<bool, Hand>(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FourOfAKind;
        List<Card> arCards = new List<Card>();

        hand.Deck = new List<Card>();
        hand.Deck.AddRange(prThreeOfAKindHand.Item2.Deck.GetRange(0, Hand.s_nThreeOfAKind));
        hand.Deck.AddRange(prPair.Item2.Deck);
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckFlush(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        Suit nDefaultSuit = ((Card)cards.Peek()).Suit;
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
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckStraight(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
            if (!arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return System.Tuple.Create<bool, Hand>(false, null);
        }

        // 特規 6 張牌
        if (arLinearSort[0] && arLinearSort[(int)CardValue.MaxValue - (int)CardValue.MinValue])
            return System.Tuple.Create<bool, Hand>(false, null);;

        Hand hand = new Hand();
        hand.HandRank = HandRank.Straight;
        List<Card> arCards = new List<Card>();
        Card[] arQueueCards = cards.ToArray();
        foreach (Card card in arQueueCards)
            arCards.Add(card);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckThreeOfAKind(Queue<Card> cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create<bool, Hand>(false, null);

        List<Card>[] arLinearSort = new List<Card>[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = new List<Card>();

        int nCount = 0;
        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
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
                for (int j = 0; j < arLinearSort.Length; ++j)
                {
                    if (Hand.s_nThreeOfAKind != arLinearSort[j].Count)
                    {
                        arCards.AddRange(arLinearSort[j]);
                        break;
                    }
                }
                break;
            }
        }
        return System.Tuple.Create<bool, Hand>(true, hand);
    }

    private static System.Tuple<bool, Hand> CheckTwoPair(Queue<Card> cards)
    {
        return System.Tuple.Create<bool, Hand>(true, null);
    }

    private static System.Tuple<bool, Hand> CheckOnePair(Queue<Card> cards)
    {
        return System.Tuple.Create<bool, Hand>(true, null);
    }
}
}  // namespace PokerGame
