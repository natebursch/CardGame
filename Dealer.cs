using System;
using System.Collections.Generic;
namespace Deckofcards
{
    public class Dealer: Player
    {
        //counting variable for how many times a method has been called.
        public int methodCounter = 0;

        //didnt make this a get and set because im lazy
        public int cardCount = 0;


        //private varables
        private string dealerName = "";

        //gets and sets
        public string DealerName { get { return this.dealerName; } set { this.dealerName = value; } }
        public int MethodCounter { get { return this.methodCounter; } set { this.methodCounter = value; } }
        public Dealer() : this("Dealer",0) { }
        public Dealer(string aName, int aPlayerID) : base(aPlayerID) { this.DealerName = aName; }

        public int DealerBlackJackValue(List<Card> hand)
        {
            int handValue = 0;
            int countedAce = 0;
            int aceCount = 0;
            foreach (var card in hand)
            {   if (cardCount == 0) { cardCount++; }
                else
                {
                    if (card.Number == 14) { handValue += 11; aceCount += 1; }
                    else if (card.Number >= 10) { handValue += 10; }
                    else { handValue += card.Number; }
                }
                if (handValue > 17 && aceCount > 0 && aceCount != countedAce) { handValue -= 10 * aceCount;countedAce = aceCount;  }

            }
            return handValue;
        }


        /*
        public string DealerWin(Dealer dealer)
        {
            string winCondition = "";
            if (dealer.DealerBlackJackValue(Hand) == 21)
            {
                
            }
            return winCondition;
        }*/

        //methods
        public override string ToString()
        {
            if (MethodCounter == 0)
            {
                string msg = "";
                int skipFirstcounter = 0;
                msg += "Dealer : " + this.DealerName + "\n" + "Hand: \n";
                foreach (var card in Hand)
                {
                    if (skipFirstcounter >= 1) { msg += "\t" + card + "\n"; }
                    else { msg += "\t" + "hidden" + "\n"; skipFirstcounter++; }
                }
                msg += "Total: " + DealerBlackJackValue(Hand) + "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
                MethodCounter++;
                return msg;
            }
            else
            {
                string msg = "";
                msg += "Dealer : " + this.DealerName + "\n" + "Hand: \n";
                foreach (var card in Hand)
                {
                    msg += "\t" + card + "\n";
                }
                msg += "Total: " + DealerBlackJackValue(Hand) + "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
                
                return msg;
            }
                
        }
        

    }
}
