namespace PokerGame
{
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

    public bool IsEndOfGame()
    {
        if(!Player1.Deck.Any())
        {
            Console.WriteLine(Player1.Name + " is out of cards!  " + Player2.Name + " WINS!");
            return true;
        }
        else if(!Player2.Deck.Any())
        {
            Console.WriteLine(Player2.Name + " is out of cards!  " + Player1.Name + " WINS!");
            return true;
        }
        else if(TurnCount > 1000)
        {
            Console.WriteLine("Infinite game!  Let's call the whole thing off.");
            return true;
        }
        return false;
    }
}
}  // namespace PokerGame
