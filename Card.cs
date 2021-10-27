using System;
namespace Deckofcards
{
    public class Card
    {
        private string suit = "n/a";
        private int number = 0;

        public string Suit { get { return this.suit; } set { this.suit = value; } }
        public int Number { get { return this.number; } set { this.number = value; } }

        

        public Card():this("n/a",0)
        {
        }
        public Card(string aSuit, int aNumber)
        {
            this.suit = aSuit;
            this.number = aNumber;
        }

        public override string ToString()
        {
            string msg = "";
            if (this.number == 11) { msg += "Jack " + this.Suit; }
            else if (this.number == 12) { msg += "Queen " + this.Suit; }
            else if (this.number == 13) { msg += "King " + this.Suit; }
            else if (this.number == 14) { msg += "Ace " + this.Suit; }
            else { msg += this.Number + " " + this.Suit; }

            
            return msg; 
        }
    }
}
