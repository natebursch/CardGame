using System;
using System.Collections.Generic;
namespace Deckofcards
{
    public class Deck
    {
        private Queue<Card> aDeckofCards = new Queue<Card>();

        public Queue<Card> ADeckofCards { get { return this.aDeckofCards; } set { this.aDeckofCards = value; } }


        public Deck()
        {
            for (int i = 2; i < 15; i++) { aDeckofCards.Enqueue(new Card("Hearts", i)); }
            for (int i = 2; i < 15; i++) { aDeckofCards.Enqueue(new Card("Diamonds", i)); }
            for (int i = 2; i < 15; i++) { aDeckofCards.Enqueue(new Card("Spades", i)); }
            for (int i = 2; i < 15; i++) { aDeckofCards.Enqueue(new Card("Clubs", i)); }

        }

        public void AddCard(Card aCard)
        {
            this.aDeckofCards.Enqueue(aCard);
        }

        public int Count()
        {
            int i = aDeckofCards.Count;
            return i;
        }

        //shuffling
        public void Shuffle(Deck aDeck)
        {
            //create an empty list
            List<Card> aListofCards = new List<Card>();
            //create a new shuffled queue
            Queue<Card> aShuffledDeck = new Queue<Card>();

            //go through the deck of cards given and add them to the list
            foreach(Card c in aDeck.ADeckofCards)
            {
                aListofCards.Add(c);
            }
            //shuffle the cards randomly and then add them to the q
            while (aListofCards.Count !=0)
            {
                Random r = new Random();
                int selected = r.Next(0, aListofCards.Count);
                aShuffledDeck.Enqueue(aListofCards[selected]);
                aListofCards.Remove(aListofCards[selected]);
                
            }
            ADeckofCards = aShuffledDeck;
        }
        //check deck size
        public Deck CheckDeck(Deck aDeck)
        {
            if (aDeck.Count() < 26) { aDeck = new Deck(); Shuffle(aDeck); Console.WriteLine("New Deck"); }
            return aDeck;
        }


        public override string ToString()
        {
            string msg = "";
            foreach (var card in ADeckofCards)
            {
                msg += card.ToString() + "\n";
            }
            return msg;
        }
    }
}
