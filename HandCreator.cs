using System.Collections;
using System.Collections.Generic;

namespace PokerGame
{
class HandCreator
{
    Hand Arrange(Queue cards)
    {
        System.Tuple<bool, Hand?> pairHand = CheckStraightFlush(cards);
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
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) =>
            (x.Value > y.Value || (x.Value == y.Value && x.Suit > y.Suit)) ? 1 :
            (x.Value < y.Value || (x.Value == y.Value && x.Suit < y.Suit)) ? -1 : 0));
        hand.Deck = arCards;
        return hand;
    }

    private static System.Tuple<bool, Hand?> CheckStraightFlush(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

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
                return System.Tuple.Create(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.StraightFlush;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckFourOfAKind(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

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
            return System.Tuple.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FourOfAKind;
        ArrayList<Card> arCards = new ArrayList<Card>();

        for (int i = 0; i < arLinearSort.Count; ++i)
        {
            if (Hand.s_nFourOfAKind == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
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
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckFullHouse(Queue cards)
    {
        System.Tuple<bool, Hand?> prThreeOfAKindHand = CheckThreeOfAKind(cards);
        if (!prThreeOfAKindHand.First)
            return System.Tuple.Create(false, null);

        Queue qPairCadidate = new Queue();
        qPairCadidate.Add(prThreeOfAKindHand.Second.Deck.GetRange(Hand.s_nThreeOfAKind, prThreeOfAKindHand.Second.Deck.Count - Hand.s_nThreeOfAKind));

        System.Tuple<bool, Hand?> prPair = CheckPair(qPairCadidate);
        if (!qPairCadidate.First)
            return System.Tuple.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.FullHouse;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(prThreeOfAKindHand.Second.Deck.GetRange(0, Hand.s_nThreeOfAKind));
        arCards.Add(qPairCadidate.Second.Deck);
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckFlush(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

        Suit nDefaultSuit = cards.Peek().Suit;
        foreach (Card card in cards)
        {
            if (nDefaultSuit != card.Suit)
                return System.Tuple.Create(false, null);
        }

        Hand hand = new Hand();
        hand.HandRank = HandRank.Flush;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckStraight(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

        bool[] arLinearSort = new bool[(int)CardValue.MaxValue - (int)CardValue.MinValue + 1];
        for (int i = 0; i < arLinearSort.Length; ++i)
            arLinearSort[i] = false;

        foreach (Card card in cards)
        {
            int nIndex = card.Value - (int)CardValue.MinValue;
            if (!arLinearSort[nIndex])
                arLinearSort[nIndex] = true;
            else
                return System.Tuple.Create(false, null);
        }

        // 特規 6 張牌
        if (arLinearSort[0] && arLinearSort[(int)CardValue.MaxValue - (int)CardValue.MinValue])
            return System.Tuple.Create(false, null);;

        Hand hand = new Hand();
        hand.HandRank = HandRank.Straight;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(cards);
        arCards.Sort(Comparer<Card>.Create((x, y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0));
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckThreeOfAKind(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

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
            return System.Tuple.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.ThreeOfAKind;
        ArrayList<Card> arCards = new ArrayList<Card>();
        
        for (int i = 0; i < arLinearSort.Count; ++i)
        {
            if (Hand.s_nPair == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
                arCards.Add(arLinearSort[i]);
                for (int j = 0; j < arLinearSort.Count; ++j)
                {
                    if (HandRank.ThreeOfAKind != arLinearSort[j])
                    {
                        arCards.Add(arLinearSort[j]);
                        break;
                    }
                }
                hand.Deck = arCards;
                break;
            }
        }
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckTwoPair(Queue cards)
    {
        System.Tuple<bool, Hand?> prOnePair = CheckOnePair(cards);
        if (!prOnePair.First)
            return System.Tuple.Create(false, null);

        Queue qPairCadidate = new Queue();
        qPairCadidate.Add(prOnePair.Second.Deck.GetRange(Hand.s_nPair, prOnePair.Second.Deck.Count - Hand.s_nPair));

        System.Tuple<bool, Hand?> prPair = CheckPair(qPairCadidate);
        if (!qPairCadidate.First)
            return System.Tuple.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.TwoPair;
        ArrayList<Card> arCards = new ArrayList<Card>();
        arCards.Add(prOnePair.Second.Deck.GetRange(0, Hand.s_nPair));
        arCards.Add(qPairCadidate.Second.Deck);
        hand.Deck = arCards;
        return System.Tuple.Create(true, hand);
    }

    private static System.Tuple<bool, Hand?> CheckOnePair(Queue cards)
    {
        if (0 == cards.Count)
            return System.Tuple.Create(false, null);

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
        if (Hand.s_nPair != nCount)
            return System.Tuple.Create(false, null);

        Hand hand = new Hand();
        hand.HandRank = HandRank.OnePair;
        ArrayList<Card> arCards = new ArrayList<Card>();
        
        for (int i = 0; i < arLinearSort.Count; ++i)
        {
            if (Hand.s_nPair == arLinearSort[i].Count)
            {
                arLinearSort[i].Sort(Comparer<Card>.Create((x, y) => x.Suit > y.Suit ? 1 : x.Suit < y.Suit ? -1 : 0));
                arCards.Add(arLinearSort[i]);
                for (int j = 0; j < arLinearSort.Count; ++j)
                {
                    if (i != j)
                        arCards.Add(arLinearSort[j]);
                }
                hand.Deck = arCards;
                break;
            }
        }
        return System.Tuple.Create(true, hand);
    }
}
}  // namespace PokerGame
