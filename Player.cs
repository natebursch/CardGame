using System;
using System.Collections.Generic;
namespace Deckofcards
{
    public class Player
    {
        private int id = -1;
        private List<Card> hand = new List<Card>();
        private int chipCount = 1000;
        private int bet = 0;
        private string playerOutcome = "";

        public int Id { get { return this.id; }set { this.id = value; } }
        public List<Card> Hand { get { return this.hand; } set { this.hand = value; } }
        public int ChipCount { get { return this.chipCount; } set { this.chipCount = value; } }
        public int Bet { get { return this.bet; } set { this.bet = value; } }
        public string PlayerOutcome { get { return this.playerOutcome; } set { this.playerOutcome = value; } }

        public Player() : this(0) { }
        public Player(int playerID){ this.Id = playerID; }


        //OVERLOADING IS IF YOU ADD TO A METHOD AFTER A BLANK METHOD
        public void Draw() { } //nbothing

        public void Draw(Deck aDeck, int amount) //overloaded***
        {
            for (int i = 0; i < amount; i++)
            {
                Hand.Add(aDeck.ADeckofCards.Dequeue());
            }
            
        }
        //THIS IS WHERE YOU SET VALUES FOR THE CARD GAME THE PLAYER 
        public int BlackJackValue(List<Card> hand)
        {
            int handValue = 0;
            int aceCount = 0;
            foreach (var card in hand)
            {
                if (card.Number == 14) { handValue += 11; aceCount += 1; }
                else if (card.Number >= 10) { handValue += 10; }
                else { handValue += card.Number; }

                if (handValue > 21 && aceCount > 0) { handValue -= 10 * aceCount; }

            }
            return handValue;
        }
        public override string ToString()
        {
            string msg = "";
            msg += "Player ID : " + this.Id + "\nPlayer Bet: " + this.Bet+ "\nHand: \n";
            foreach (var card in Hand)
            {
                msg += "\t" + card + "\n";
            }
            if (BlackJackValue(Hand) <= 21) { msg += "Total: " + BlackJackValue(Hand) + "\n"; }
            else if (BlackJackValue(Hand) > 21) { msg += "Total: BUST\n"; }
            msg += "Chip Count: " + ChipCount + "\n~~~~~~~~~~~~~~~~~~~~~~~~";
            return msg; 
        }
    }
}
