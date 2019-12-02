public void PlayTurn()
{
    Queue<Card> pool = new Queue<Card>();

    //Step 1: Each player flips a card
    var player1card = Player1.Deck.Dequeue();
    var player2card = Player2.Deck.Dequeue();

    pool.Add(player1card);
    pool.Add(player2card);

    Console.WriteLine(Player1.Name + " plays " + player1card.DisplayName + ", " + Player2.Name + " plays " + player2card.DisplayName);
 
    //Step 2: If the cards have the same value, we have a War!
    //IMPORTANT: We CONTINUE to have a war as long as the flipped cards are the same value.
    while (player1card.Value == player2card.Value)
    {
        Console.WriteLine("WAR!");
        
        //If either player doesn't have enough cards for the War, they lose.
        if (Player1.Deck.Count < 4)
        {
            Player1.Deck.Clear();
            return;
        }
        if(Player2.Deck.Count < 4)
        {
            Player2.Deck.Clear();
            return;
        }
                
        //Add three "face-down" cards from each player to a common pool
        pool.Add(Player1.Deck.Dequeue());
        pool.Add(Player1.Deck.Dequeue());
        pool.Add(Player1.Deck.Dequeue());
        pool.Add(Player2.Deck.Dequeue());
        pool.Add(Player2.Deck.Dequeue());
        pool.Add(Player2.Deck.Dequeue());

        //Pop the fourth card from each player's deck
        player1card = Player1.Deck.Dequeue();
        player2card = Player2.Deck.Dequeue();
        
        pool.Enqueue(player1card);
        pool.Enqueue(player2card);

        Console.WriteLine(Player1.Name + " plays " + player1card.DisplayName + ", " + Player2.Name + " plays " + player2card.DisplayName);
    }

    //Add the won cards to the winning player's deck, and display which player won that hand.  This uses our custom extension method from earlier.
    if(player1card.Value < player2card.Value)
    {
        Player2.Deck.Enqueue(pool);
        Console.WriteLine(Player2.Name + " takes the hand!");
    }
    else
    {
        Player1.Deck.Enqueue(pool);
        Console.WriteLine(Player1.Name + " takes the hand!");
    }

    TurnCount++;
}
