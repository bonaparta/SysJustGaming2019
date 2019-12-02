public class Game
{
    private Player Player1;
    private Player Player2;

    public Game(string player1name, string player2name)
    {
        Player1 = new Player(player1name);
        Player2 = new Player(player2name);

        var cards = DeckCreator.CreateCards(); //Returns a shuffled set of cards

        var deck = Player1.Deal(cards); //Returns Player2's deck.  Player1 keeps his.
        Player2.Deck = deck;
    }
}
