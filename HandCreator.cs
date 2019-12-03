class HandCreator
{
    Tuple<Hand, List<Card>> Arrange(Queue<Card> cards)
    {
        Tuple<bool, Hand> pairHand = CheckStraightFlush(cards);
        if (pairHand.Item1)
        {
            Hand hand = new Hand();
            hand.HandRank = HandRank.StraightFlush;
            ArrayList<Card> arCards = new ArrayList<Card>();
            arCards.Add(cards);
            arCards.Sort();
            Tuple<Hand, List<Card>> retData = Tuple.Create(hand, );
        }
    }

    private static Tuple<bool, Hand?> CheckStraightFlush(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckStaightFlush(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckFourOfAKind(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckFullHouse(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckFlush(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckThreeOfAKind(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckTwoPair(Queue<Card> cards) {
        return true;
    }

    private static Tuple<bool, Hand?> CheckOnePair(Queue<Card> cards) {
        return true;
    }
}
